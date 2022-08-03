using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : PanelBase
{
    private void OnEnable()
    {
        EventSystem.StartListening("OnLevelStart", "InGameUI", ChangeLevelText);
    }
    private void OnDisable()
    {
        EventSystem.StopListening("OnLevelStart", "InGameUI");
    }
    // Start is called before the first frame update
    void ChangeLevelText(object[] obj)
    {
        GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Level #"+(GameManager.Instance.CurrentLevel+1);
    }

}
