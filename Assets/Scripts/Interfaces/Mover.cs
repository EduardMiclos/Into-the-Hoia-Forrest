using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Mover {

    /* The object's box collider is required in order
    to check for collisions inside the BoxCast function. */
    BoxCollider2D boxCollider {get; set;}

    /* Difference of position between frames. */
    Vector3 deltaTranslate {get; set;}

    /* We use this in order to cast the 2D box
    and check for collisions. */
    RaycastHit2D raycastHit {get; set;}

    /* Every mover has a certain speed in x and y directions. */
    Speed speed {get; set;}

    Animator animator {get; set;}

    /* To be implemented inside Actor class. */
    void Move(Vector3 input);
}