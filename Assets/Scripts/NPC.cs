using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

/* This is a class used for all the STATIC NPCs,
meaning NPC that are not characterized by movement or
walking animations and contribute to the story of the game. */
public class NPC : Collidable
{
    public Dialogue dialogue;
    private Light2D light2d;
    private bool hadFirstInteraction = false;

    protected override void Start()
    {
        base.Start();
        MarkAsInteractable();
    }

    protected override void OnCollide(Collider2D collider)
    {
        if(hadFirstInteraction == false && collider.tag.Equals("Player"))
        {
            OnDialogStart();
            ResetLightIntensity();

            DialogueManager.getInstance().StartDialogue(this);
            hadFirstInteraction = true;
        }
    }

    private void ResetLightIntensity()
    {
        light2d.intensity = 0.0f;
    }

    private IEnumerator ChangeLightIntensity()
    {
        bool gradientDirectionUp = true;

        while (hadFirstInteraction == false)
        {
            if (light2d.intensity <= 0f)
            {
                gradientDirectionUp = true;
            }

            if (light2d.intensity >= 0.4f)
            {
                gradientDirectionUp = false;
            }


            if (gradientDirectionUp == true)
            {
                light2d.intensity += 0.01f;
            }
            else
            {
                light2d.intensity -= 0.01f;
            }

            yield return new WaitForSeconds(0.01f);
        }

    }

    private void MarkAsInteractable()
    {
        light2d = GetComponent<Light2D>();
        StartCoroutine(ChangeLightIntensity());
    }


    /* This may or may not be implemented differently
    depending on the NPC. */
    internal virtual void OnDialogStart() {}

    /* This may or may not be implemented differently
    depending on the NPC. */
    internal virtual void OnDialogEnd() {}
}
