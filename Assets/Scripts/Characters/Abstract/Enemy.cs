using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Actor
{
    private Transform playerTransform;
    private float minimumDistance = 5f;

    public override void Start()
    {
        base.Start();
        playerTransform = GameObject.Find("Player").transform;

        speed = new Speed(3f, 3f);

        blockingLayers = new string[] { "Blocking"};
    }

    private void FollowPlayer()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) < minimumDistance)
        {
            Move(-playerTransform.position.normalized);
        }

        //Move(Vector3.MoveTowards(this.transform.position, playerTransform.position, 0.1f));
    }

    public void FixedUpdate()
    {
        FollowPlayer();
    }
}
