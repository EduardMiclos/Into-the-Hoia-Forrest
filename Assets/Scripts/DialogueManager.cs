using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* This is DialogueManager is a Singleton. */
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance = null;

    private Queue<string> sentences;

    public Text nameText;
    public Text dialogText;

    public Animator animator;

    private bool activeDialogue = false;

    private DialogueManager() {}
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        sentences = new Queue<string>();    
    }


    public void StartDialogue(Dialogue dialogue)
    {
        if (activeDialogue == false)
        {
            animator.SetBool("IsOpen", true);
            activeDialogue = true;
            nameText.text = dialogue.name;

            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count != 0)
        {
            string sentence = sentences.Dequeue();

            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        activeDialogue = false;
    }

}
