using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RePlayButton : MonoBehaviour
{
    Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClicked);
    }
    private void ButtonClicked()
    {
        SceneManager.LoadScene(0);
        UIManager.Instance.HidePanel("FailUI");

    }
}
