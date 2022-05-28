using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance = null;
    private InventoryManager() {}

    public Inventory playerInventory;
    public InventoryUI UIinventory;

    public WeaponUI weaponUI;

    internal bool isInventoryOpen;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        isInventoryOpen = false;
    } 

    internal void OpenInventory()
    {
        UIinventory.OpenAnimation();
        isInventoryOpen = true;
    }

    internal void CloseInventory()
    {
        UIinventory.CloseAnimation();
        isInventoryOpen = false;
    }

    public bool AddInventoryItem(Item item)
    {
        
        if (playerInventory.backpackItems.Count < playerInventory.backpackCapacity)
        {
            UIinventory.AddBackpackItem(item, playerInventory.backpackItems.Count);
            playerInventory.AddBackpackItem(item);
            return true;
        }

        if (playerInventory.primaryItems.Count < playerInventory.primaryCapacity)
        {
            playerInventory.AddPrimaryItem(item);
            return true;
        }

        return false;
    }

    public void AddWeapon()
    {
        AddWeapon(new Weapon());
    }

    public void AddWeapon(Weapon givenWeapon)
    {
        if (playerInventory.primaryWeapon == null)
        {
            playerInventory.primaryWeapon = givenWeapon;
            weaponUI.PlayUpgradeSound();
            UIinventory.AddPrimaryWeapon(givenWeapon);
        }
        else
        {
            AddInventoryItem(givenWeapon);
        }

        weaponUI.spriteRenderer.sprite = givenWeapon.sprite;
    }
}
