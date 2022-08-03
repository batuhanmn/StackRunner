using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicedStack: MonoBehaviour
{
    public void CreateStack(Color stackColor)
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.Slice);
        gameObject.GetComponent<Renderer>().material.color = stackColor;
        gameObject.SetActive(true);
        gameObject.AddComponent<Rigidbody>();
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        //play your sound
        yield return new WaitForSeconds(3); //waits 3 seconds
        Destroy(gameObject); //this will work after 3 seconds.
    }
}
