using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* This is DialogueManager is a Singleton. */
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance = null;

    /* We make the constructor private so we can
    control the number of instances of this object. */
    private DialogueManager() {}

    private NPC currentNPC;
    private Queue<string> sentences;
    public Text nameText;
    public Text dialogText;
    public Animator animator;
    public bool activeDialogue = false;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        sentences = new Queue<string>();    
    }

    public static DialogueManager GetInstance()
    {
        if (instance == null)
        {
            GameObject gameObject = new GameObject();
            instance = gameObject.AddComponent<DialogueManager>();
        }
        return instance;
    }

    public void StartDialogue(NPC npc)
    {
        if (activeDialogue == false)
        {
            currentNPC = npc;
            activeDialogue = true;

            nameText.text = npc.dialogue.title;

            sentences.Clear();

            foreach (string sentence in npc.dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            animator.SetBool("isOpen", true);
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

    /* This is a Coroutine. Basically, we write
    a new letter every 0.02 seconds in order to give
    it an animation effect. */
    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    private void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        activeDialogue = false;

        currentNPC.OnDialogEnd();
        currentNPC = null;
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && activeDialogue == true)   
        {
            DisplayNextSentence();
        }
    }

}
