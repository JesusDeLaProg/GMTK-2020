using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCheck : MonoBehaviour
{
    public void CallEndAnim()
    {
        FindObjectOfType<AnimController>().AnimationIsOver();
        Debug.Log("Yo");
    }
}
