using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    protected BoxCollider2D boxCollider;

    protected ContactFilter2D filter;

    /* Here we store the number of hits per frame. */
    protected Collider2D[] collisionResults;
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        collisionResults = new Collider2D[10];
    }

    protected virtual void Update()
    {
        boxCollider.OverlapCollider(filter, collisionResults);

        for (int i = 0; i < collisionResults.Length; i++)
        {
            if (collisionResults[i] != null)
            {
                OnCollide(collisionResults[i]);
            }

            collisionResults[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D coll) {}
}
