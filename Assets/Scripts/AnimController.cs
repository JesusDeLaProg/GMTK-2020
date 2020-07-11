using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    public bool StartIntro = false;
    bool IntroStarted = false;
    bool AnimEnded = false;
    public bool StartOutro = false;

    public Animator animator;

    private void Update()
    {
        if (StartIntro)
        {
            StartIntro = false;
            IntroStarted = true;
            animator.Play("DialogueIn");
        }
        if (IntroStarted)
        {
            if (AnimEnded)
            {

                FindObjectOfType<DialogueTrigger>().TriggerDialogue();

            }

        }
    }

    public void AnimationIsOver()
    {
        AnimEnded = true;
    }

}
