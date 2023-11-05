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
        Vector3 moveVector = playerTransform.position - transform.position;
        moveVector = moveVector.normalized;

        if (Vector3.Distance(transform.position, playerTransform.position) < minimumDistance)
        {
            Move(moveVector);
        }
    }

    public void FixedUpdate()
    {
        FollowPlayer();
    }
}
