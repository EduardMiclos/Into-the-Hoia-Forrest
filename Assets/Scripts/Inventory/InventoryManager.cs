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

    private Item IsItemInSubinventory(Item item, List<Item> subInventory, out int itemIndex)
    {
        int i = -1;
        itemIndex = -1;

        foreach (Item itrItem in subInventory)
        {
            i++;

            if (itrItem != null && itrItem.typeOfItem == item.typeOfItem)
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
    
    private Item IsItemInBackpack(Item item, out int itemIndex)
    {
        return IsItemInSubinventory(item, playerInventory.backpackItems, out itemIndex);
    }

    private Item IsItemInPrimary(Item item, out int itemIndex)
    {
        return IsItemInSubinventory(item, playerInventory.primaryItems, out itemIndex);
    }

    public bool AddInventoryItem(Item item)
    {
        Item matchedItem;
        int itemIndex;

        if ((matchedItem = IsItemInBackpack(item, out itemIndex)) != null)
        {
            matchedItem.IncreaseAmount();
            UIInventory.SetBackpackItemAmount(matchedItem.amount, itemIndex);
            return true;
        }

        if ((matchedItem = IsItemInPrimary(item, out itemIndex)) != null)
        {
            matchedItem.IncreaseAmount();
            UIInventory.SetPrimaryItemAmount(matchedItem.amount, itemIndex);
            return true;
        }

        if (playerInventory.freeBackpackSlots.Count > 0)
        {
            
            int itemSlot = playerInventory.AddBackpackItem(item);

            if (itemSlot != -1)
            {
                UIInventory.AddBackpackItem(item, itemSlot);
                return true;
            }

            return false;
        }

        if (playerInventory.primaryItems.Count > 0)
        {
            int itemSlot = playerInventory.AddPrimaryItem(item);

            if (itemSlot != -1)
            {
                UIInventory.AddPrimaryItem(item, itemSlot);
                return true;
            }

            return false;
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
