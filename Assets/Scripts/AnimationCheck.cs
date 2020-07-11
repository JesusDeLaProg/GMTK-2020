using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCheck : MonoBehaviour
{
    public void CallEndAnim()
    {
        FindObjectOfType<AnimController>().AnimationIsOver();
    }
    public void Reset()
    {
         FindObjectOfType<AnimController>().Reset();
         gameObject.GetComponent<Animator>().SetBool("DialogueEnding", false);
    }
}
