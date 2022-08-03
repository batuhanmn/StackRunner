using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public Cinemachine.CinemachineFreeLook camera;
    public float speed;
    private bool isRotate;
    private void OnEnable()
    {
        EventSystem.StartListening("OnLevelFinish","CameraRotate", CameraRotate);
    }
    private void OnDisable()
    {
        EventSystem.StopListening("OnLevelFinish", "CameraRotate");
    }
    private void CameraRotate(object[] obj)
    {
        isRotate = true;

    }
    private void Update()
    {
        if (!isRotate) return;
        camera.gameObject.SetActive(true);
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
