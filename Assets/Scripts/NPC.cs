using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This is a class used for all the STATIC NPCs,
meaning NPC that are not characterized by movement or
walking animations and contribute to the story of the game. */
public class NPC : Collidable
{
    public Dialogue dialogue;
    
    protected override void OnCollide(Collider2D collider)
    {
        if(collider.tag.Equals("Player"))
        {
            DialogueManager.instance.StartDialogue(dialogue);
        }
    }
}
