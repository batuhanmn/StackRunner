using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState
{
    Dance,
    Run,
    Idle
}
public class CharacterController : MonoBehaviour
{

    private CharacterState characterState = CharacterState.Idle;
    public float movementSpeed = 0;
    public float MovementSpeed
    {
        get { return movementSpeed == 0 ? movementSpeed = GameManager.Instance.CharacterMovementConstant * GameManager.Instance.Speed : movementSpeed; }
    }
    private void OnEnable()
    {
        EventSystem.StartListening("StackPlaced", "MoveStart", Run);
        EventSystem.StartListening("OnLevelFinish", "CharacterControllerLevelFinish", Dance);
    }
    private void OnDisable()
    {
        EventSystem.StopListening("StackPlaced", "MoveStart");
        EventSystem.StopListening("OnLevelFinish", "CharacterControllerLevelFinish");
    }
    private void Run(object[] obj)
    {
        characterState = CharacterState.Run;
    }
    private void Dance(object[] obj)
    {
        characterState = CharacterState.Dance;
        gameObject.GetComponentInChildren<Animator>().SetBool("isDancing", true);
    }

    private void Update()
    {
        if(characterState==CharacterState.Run) transform.Translate(Vector3.forward * Time.deltaTime * MovementSpeed);

    }
}
