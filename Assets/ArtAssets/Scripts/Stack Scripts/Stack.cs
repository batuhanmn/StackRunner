using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StackStatus
{
    Ready=5,
    Mover=10,
    Placed=15
}
public class Stack : MonoBehaviour
{
    public StackStatus Status = StackStatus.Ready;
    public GameObject SlicedStackGameObject;
    private Vector3 route = Vector3.right;
    private Color stackColor = Color.white;
    private float Tolerance = 0;

    public float movementSpeed = 0;
    public float MovementSpeed
    {
        get { return movementSpeed == 0 ? movementSpeed = GameManager.Instance.StackMovementConstant * GameManager.Instance.Speed : movementSpeed; }
    }

    // Start is called before the first frame update

    void Start()
    {
        
        Tolerance = GameManager.Instance.StackTolerance;


        if (transform.position.x > 0)
        {
            route = Vector3.left;
        }
        stackColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        gameObject.GetComponent<Renderer>().material.color = stackColor;
    }

    private void Update()
    {
        if(Status == StackStatus.Mover)
        Move();
    }
    public void Move()
    {
        transform.Translate(route * Time.deltaTime* MovementSpeed);
    }
    public void Placed()
    {
        if (Status == StackStatus.Placed) return;
        EventSystem.TriggerEvent("StackPlaced");
        bool isSliced = false;
        Transform oldTransform = StackController.Instance.oldStack.transform;
        float xPosition = (transform.position.x- oldTransform.position.x)/2+ oldTransform.position.x;
        
        Status = StackStatus.Placed;
        float n_xDist = gameObject.transform.localScale.x / 2;
        float o_xDist = oldTransform.localScale.x / 2;
        if (Mathf.Abs(xPosition - oldTransform.position.x) > o_xDist) //Fail
        {
            Status = StackStatus.Ready;
            EventSystem.TriggerEvent("OnLevelFinish", false);
            return;
        }
        float new_xScale = 0;
        float sliced_xScale = 0;
        float sliced_xPosition = 0;

        
        if (transform.position.x > oldTransform.position.x) //merkezin sağında 
        {
            new_xScale = (oldTransform.position.x + o_xDist) - (transform.position.x - n_xDist);
            if (transform.position.x - oldTransform.position.x < Tolerance)
            {
                SuccessPlaced();
            }
            else
            {
                isSliced = true;
                sliced_xScale = oldTransform.localScale.x - new_xScale;
                sliced_xPosition = xPosition + transform.localScale.x / 2;
            }
            
        }
        else if(transform.position.x < oldTransform.position.x) // merkezin solunda 
        {
            new_xScale = (transform.position.x + n_xDist) - (oldTransform.position.x - o_xDist) ;
            if (oldTransform.position.x- transform.position.x  < Tolerance)
            {
                SuccessPlaced();
            }
            else
            {
                isSliced = true;
                sliced_xScale = oldTransform.localScale.x - new_xScale;
                sliced_xPosition = xPosition - transform.localScale.x / 2;
            }

        }
        else
        {
            SuccessPlaced();
        }

        if (isSliced)
        {
            Vector3 slicedObjectPosition = new Vector3(sliced_xPosition, transform.position.y, transform.position.z);
            transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
            transform.localScale = new Vector3(new_xScale, transform.localScale.y, transform.localScale.z);
            GameObject slicedStackGameObject = Instantiate(SlicedStackGameObject);
            slicedStackGameObject.GetComponent<SlicedStack>().CreateStack(stackColor);
            slicedStackGameObject.transform.localScale = new Vector3(sliced_xScale, transform.localScale.y, transform.localScale.z);
            slicedStackGameObject.transform.position = slicedObjectPosition;
        }
        
    }
    private void SuccessPlaced()
    {
        StackController.Instance.FinalWidth = transform.localScale.x;
        transform.position = new Vector3(StackController.Instance.oldStack.transform.position.x, transform.position.y, transform.position.z);
        Debug.Log("tam tam");
        SoundManager.Instance.PlaySound(SoundManager.Sound.Classic);
    }

}
