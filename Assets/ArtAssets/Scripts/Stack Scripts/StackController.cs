using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : Singleton<StackController>
{
    
    public GameObject FinishStack;
    public GameObject Stack;
    public int RoadLenght;
    public List<GameObject> Stacks;
    public Queue<GameObject> StackQueue = new Queue<GameObject>();
    private Stack activeStack=null;
    public Stack oldStack=null;
    public AudioClip StackedSound;
    public float FinalWidth;

    private void Start()
    {
        EventSystem.StartListening("OnLevelStart", "StackController", SkipQueue) ;
    }

    private void OnDisable()
    {
        EventSystem.StopListening("OnLevelStart", "StackController");
    }

    private void SkipQueue(object[] obj)
    {
        SkipQueue();
    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            if (!GameManager.Instance.isLevelStart) return;
            if (activeStack)
            {
                activeStack.Placed();
                oldStack = activeStack;
            }

            SkipQueue();
        }
    }

    public void CreateRoad(int StackLenght)
    {
        RoadLenght = StackLenght;
        float zPos = 0;
        for (int i = 0; i < RoadLenght; i++)
        {
            GameObject tmp_go = Instantiate(Stack, transform);

            Vector3 oldTransform = Stacks[i].transform.position;
            var val = Random.value;
            float xPos = 0f;
            if (val < 0.5f)
            {
                xPos = -6;
            }
            else if (val > 0.5f)
            {
                xPos = 6;
            }
            zPos = oldTransform.z + Stack.GetComponent<Renderer>().bounds.size.z;
            tmp_go.transform.position = new Vector3(xPos, oldTransform.y, zPos);
            tmp_go.SetActive(false);
            StackQueue.Enqueue(tmp_go);
            Stacks.Add(tmp_go);
        }
        GameObject finish = Instantiate(FinishStack, transform);
        finish.transform.position = new Vector3(0, 0, zPos + Stack.GetComponent<Renderer>().bounds.size.z/2 + finish.GetComponent<Renderer>().bounds.size.z/2);
    }


    public void SkipQueue()
    {
        if (StackQueue.Count <= 0)
        {
            Debug.Log("Queue empty");
            return;
        }
        
        GameObject nextStack =  StackQueue.Dequeue();
        if(oldStack == null)
        {
            oldStack = Stacks[0].GetComponent<Stack>();
        }
        nextStack.transform.localScale = oldStack.transform.localScale;
        nextStack.SetActive(true);
        activeStack = nextStack.GetComponent<Stack>();
        activeStack.Status = StackStatus.Mover;
        
    }

    
}
