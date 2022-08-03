using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public enum LevelType { Default }

[System.Serializable]
public class Level
{
    public int StackLength;

    public float MoveSpeed;

    public float StackTolerance;

}

[CreateAssetMenu(fileName = "LevelData", menuName = "Level/Data", order = 1)]
public class LevelData : ScriptableObject
{
    public List<Level> Levels = new List<Level>();

}