using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    private Inventory playerInventory;
    public override void Start()
    {
        base.Start();
        speed = new Speed(4f, 4f);
    }

    private void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Move(new Vector3(moveX, moveY, 0));
    }
}
