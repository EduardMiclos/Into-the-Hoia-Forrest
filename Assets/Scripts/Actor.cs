using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour, Mover {

    public BoxCollider2D boxCollider {get; set;}
    public Vector3 deltaTranslate {get; set;}
    public RaycastHit2D raycastHit {get; set;}
    public Speed speed {get; set;}

    public Animator animator {get; set;}

    private void swapSprite()
    {
        transform.localScale = new Vector3(deltaTranslate.x < 0 ? -1f : 1f, 1f, 1f);
    }

    public virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    protected void startWalkAnimation()
    {
        animator.SetBool("isWalking", true);
    }

    protected void stopWalkAnimation()
    {
        animator.SetBool("isWalking", false);
    }

    public void Move(Vector3 input)
    {
        deltaTranslate = new Vector3 (speed.x * input.x, speed.y * input.y, 0);

        /* If the Actor is moving:
            - to the left, then the sprite is facing left.
            - to the right, then the sprite is facing right. 
        We swap the sprite accordingly. */
        if (deltaTranslate.x != 0)
        {
            swapSprite();
        }
        
        if (deltaTranslate != Vector3.zero)
        {
            startWalkAnimation();
        }
        else
        {
            stopWalkAnimation();
        }


        /* Here we are making sure that the actor can move in the y-direction (up or down),
        by casting a box there first. If the box returns null, the actor is free to move. */
        raycastHit = Physics2D.BoxCast(
            origin: transform.position,
            size: boxCollider.size,
            angle: 0,
            direction: new Vector2(0, deltaTranslate.y),
            distance: Mathf.Abs(deltaTranslate.y * Time.deltaTime),
            layerMask: LayerMask.GetMask("Blocking", "NPC", "Actor")
        );

        /* If the raycast is null, it means that 
        the actor didn't collide with anything. */
        if (raycastHit.collider == null)
        {
            /* Making the Actor move. We are multiplying with
            Time.deltaTime in order to make the translation independent
            of the framerate. */
            transform.Translate(0, deltaTranslate.y * Time.deltaTime, 0);
        }

        /* Here we are making sure that the actor can move in the x-direction (left or right),
        by casting a box there first. If the box returns null, the actor is free to move. */
        raycastHit = Physics2D.BoxCast(
            origin: transform.position,
            size: boxCollider.size,
            angle: 0,
            direction: new Vector2(deltaTranslate.x, 0),
            distance: Mathf.Abs(deltaTranslate.x * Time.deltaTime),
            layerMask: LayerMask.GetMask("Blocking", "NPC", "Actor")
        );

        if (raycastHit.collider == null)
        {
            transform.Translate(deltaTranslate.x * Time.deltaTime, 0, 0);
        }
    }
}