using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    private Inventory inventory;

    public override void Start()
    {
        base.Start();

        speed = GameManager.instance.initialPlayerSpeed;
        inventory = InventoryManager.instance.playerInventory;
    }

    private void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Move(new Vector3(moveX, moveY, 0));
    }

    // public void AddWeapon()
    // {   Weapon weapon;

    //     weapon = Instantiate(weapon, this.transform);
    //     weapon.Upgrade();
    // }
}
