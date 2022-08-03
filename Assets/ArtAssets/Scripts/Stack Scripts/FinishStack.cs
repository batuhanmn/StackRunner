using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishStack : MonoBehaviour
{
    public bool isSuccess;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            EventSystem.TriggerEvent("OnLevelFinish", isSuccess);
            Debug.Log("Oyunun sonuna geldin evlat! "+ isSuccess);
        }
    }
}
