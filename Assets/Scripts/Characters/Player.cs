using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    public Inventory inventory { get; set; }

    public override void Start()
    {
        base.Start();
        DontDestroyOnLoad(gameObject);

        speed = GameManager.instance.initialPlayerSpeed;
        inventory = InventoryManager.instance.playerInventory;

        blockingLayers = new string[]{ "Blocking", "Enemy"};
    }

    private void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Move(new Vector3(moveX, moveY, 0));
    }
}
