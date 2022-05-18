using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    public override void Start()
    {
        base.Start();
        speed = new Speed(4f, 4f);
    }

    private void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        //transform.Translate(moveX * Time.deltaTime, moveY * Time.deltaTime ,0);

        Move(new Vector3(moveX, moveY, 0));


    }
}