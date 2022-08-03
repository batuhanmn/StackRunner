using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelButton : MonoBehaviour
{
    Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClicked);
    }
    private void ButtonClicked()
    {
        UIManager.Instance.HidePanel("SuccessUI");
        SceneManager.LoadScene(0);
    }
}
