using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class GameManager : Singleton<GameManager>
{
    public LevelData LevelCollection;
    public bool isLevelStart;
    public float Speed;
    [SerializeField]
    public float StackMovementConstant = 5;
    [SerializeField]
    public float CharacterMovementConstant = 2;
    public float StackTolerance;
    public SoundAudioClip[] soundAudioClipArray;

    private int currentLevel=-1;
    public int CurrentLevel
    {
        get { return currentLevel == -1 ? currentLevel = PlayerPrefs.GetInt("CurrentLevel")% LevelCollection.Levels.Count : currentLevel; }
        set { currentLevel = value; }
    }
    private void Start()
    {
        Speed = LevelCollection.Levels[CurrentLevel].MoveSpeed;
        StackTolerance = LevelCollection.Levels[CurrentLevel].StackTolerance;
        UIManager.Instance.ShowPanel("MainMenu");
        StackController.Instance.CreateRoad(LevelCollection.Levels[CurrentLevel].StackLength);
    }
    private void OnEnable()
    {
        EventSystem.StartListening("OnLevelFinish", "GameManager", GameFinished);
    }
    private void OnDisable()
    {
        EventSystem.StopListening("OnLevelFinish", "GameManager");
    }

    private void GameFinished(object[] obj)
    {
        isLevelStart = false;
        if ((bool)obj[0] == true)
        {
            PlayerPrefs.SetInt("CurrentLevel", CurrentLevel + 1);
            PlayerPrefs.SetFloat("FinalWidth", StackController.Instance.FinalWidth);
            UIManager.Instance.ShowPanel("SuccessUI");
        }
        else
        {
            UIManager.Instance.ShowPanel("FailUI");
        }
    }

    public void IsLevelStart()
    {
        
        isLevelStart = true;
        EventSystem.TriggerEvent("OnLevelStart");
    }

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
}
