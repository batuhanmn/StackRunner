using UnityEngine;

public abstract class PanelBase : MonoBehaviour
{
    public virtual void ShowPanel(params object[] Variables)
    {
        gameObject.SetActive(true);
    }

    public virtual void HidePanel()
    {
        gameObject.SetActive(false);
    }

}