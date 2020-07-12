using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text txt;
    public Animator animator;
    Queue<string> sentences;
    AudioSource AS;



    void Start()
    {
        sentences = new Queue<string>();
        AS = gameObject.GetComponent<AudioSource>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        txt.text = "";
        foreach(char l in sentence.ToCharArray())
        {
            txt.text += l;
            PlaySound();
            yield return new WaitForSeconds(0.0025f);
        }
        yield return new WaitForSeconds(2f);
        DisplayNextSentence();
    }
    void EndDialogue()
    {
        txt.text = "";
        animator.SetBool("DialogueEnding", true);
    }

    void PlaySound()
    {
        AS.Play(0);
    }

}
