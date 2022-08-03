using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClicked);
    }
    private void ButtonClicked()
    {
        
        GameManager.Instance.IsLevelStart();
        UIManager.Instance.HidePanel("MainMenu");
    }
}
