using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance = null;
    private InventoryManager() {}

    public Inventory playerInventory;
    public InventoryUI UIInventory;
    public InventoryMenu inventoryMenu;

    internal bool isInventoryOpen;

    public WeaponUI visualWeaponObject;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    internal void OpenInventory()
    {
        UIInventory.OpenAnimation();
        isInventoryOpen = true;
    }

    internal void CloseInventory()
    {
        UIInventory.CloseAnimation();
        isInventoryOpen = false;
    }

    private Item ItemInSubinventory(Item item, List<Item> subInventory, out int itemIndex)
    {
        int i = -1;
        itemIndex = -1;

        foreach (Item itrItem in subInventory)
        {
            i++;
            if (itrItem.typeOfItem == item.typeOfItem)
            {
                if ((itrItem.typeOfItem == ItemType.Weapon) &&
                    ((Weapon)itrItem).weaponSkill != ((Weapon)item).weaponSkill)
                {
                    continue;
                }

                itemIndex = i;
                return itrItem;
            }
        }

        return null;
    }
    
    private Item ItemInBackpack(Item item, out int itemIndex)
    {
        return ItemInSubinventory(item, playerInventory.backpackItems, out itemIndex);
    }

    private Item ItemInPrimary(Item item, out int itemIndex)
    {
        return ItemInSubinventory(item, playerInventory.primaryItems, out itemIndex);
    }

    public bool AddInventoryItem(Item item)
    {
        Item matchedItem;
        int itemIndex;

        if ((matchedItem = ItemInBackpack(item, out itemIndex)) != null)
        {
            matchedItem.IncreaseAmount();
            UIInventory.SetBackpackItemAmount(matchedItem.amount, itemIndex);
            return true;
        }

        if ((matchedItem = ItemInPrimary(item, out itemIndex)) != null)
        {
            matchedItem.IncreaseAmount();
            UIInventory.SetPrimaryItemAmount(matchedItem.amount, itemIndex);
            return true;
        }

        if (playerInventory.backpackItems.Count < playerInventory.backpackCapacity)
        {
            UIInventory.AddBackpackItem(item, playerInventory.backpackItems.Count);
            playerInventory.AddBackpackItem(item);

            return true;
        }

        if (playerInventory.primaryItems.Count < playerInventory.primaryCapacity)
        {
            UIInventory.AddPrimaryItem(item, playerInventory.primaryItems.Count);
            playerInventory.AddPrimaryItem(item);

            return true;
        }

        return false;
    }

    public void AddDefaultWeapon()
    {
        AddWeapon(new Weapon());
    }

    public void AddWeapon(Weapon givenWeapon)
    {
        if (playerInventory.primaryWeapon == null)
        {
            playerInventory.primaryWeapon = givenWeapon;
            visualWeaponObject.PlayUpgradeSound();

            UIInventory.AddPrimaryWeapon(givenWeapon);
        }
        else
        {
            Weapon playerWeapon = (Weapon)playerInventory.primaryWeapon;
            if (givenWeapon.weaponSkill == playerWeapon.weaponSkill)
            {
                playerWeapon.IncreaseAmount();
                UIInventory.SetPrimaryWeaponAmount(playerWeapon.amount);
            }
            else
            {
                AddInventoryItem(givenWeapon);
            }

        }

        visualWeaponObject.spriteRenderer.sprite = givenWeapon.sprite;
    }
}
