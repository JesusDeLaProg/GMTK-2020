﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    public bool StartAnim = false;
    bool AnimStarted = false;
    bool AnimEnded = false;
    public bool StartOutro = false;
    bool soloCheck = false;

    public Animator animator;

    private void Update()
    {
        if (StartAnim)
        {
            StartAnim = false;
            AnimStarted = true;
            animator.Play("DialogueIn");
        }
        if (AnimStarted)
        {
            if (AnimEnded &&!soloCheck)
            {
                soloCheck=true;
                FindObjectOfType<DialogueTrigger>().TriggerDialogue();
            }

        }
    }

    public void AnimationIsOver()
    {
        AnimEnded = true;
    }

    public void Reset()
    {
        StartAnim = false;
        AnimStarted = false;
        AnimEnded = false;
        StartOutro = false;
        soloCheck = false;
    }

}
