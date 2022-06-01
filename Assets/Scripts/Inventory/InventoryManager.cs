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
        visualWeaponObject.SetSprite(givenWeapon.sprite);
    }

    public void InterchangeItems(GameObject currentItemSlot, GameObject targetSlot, InventoryType inventoryType1, int slotIndex1, InventoryType inventoryType2, int slotIndex2)
    {
        if (playerInventory.InterchangeItems(inventoryType1, slotIndex1, inventoryType2, slotIndex2) == true)
        {
            UIInventory.InterchangeItems(inventoryType1, currentItemSlot, targetSlot);
        }
    }

    public void MoveItemToF(GameObject currentItemSlot, InventoryType inventoryType, int slotIndex)
    {
        GameObject FSlot = GameManager.GetObjectChild(UIInventory.PrimaryItems, "0");
        InterchangeItems(currentItemSlot, FSlot, inventoryType, slotIndex, InventoryType.Primary, 0);
        UIInventory.DisplayActiveSlot(inventoryMenu.currentAttachmentObject, false);
    }

    public void MoveItemToG(GameObject currentItemSlot, InventoryType inventoryType, int slotIndex)
    {
        GameObject GSlot = GameManager.GetObjectChild(UIInventory.PrimaryItems, "1");
        InterchangeItems(currentItemSlot, GSlot, inventoryType, slotIndex, InventoryType.Primary, 1);
        UIInventory.DisplayActiveSlot(inventoryMenu.currentAttachmentObject, false);
    }

    public Item DropItemUnit(GameObject currentItemSlot, InventoryType inventoryType, int slotIndex)
    {
        Item targetItem = null;

        switch (inventoryType)
        {
            case InventoryType.Backpack:
                targetItem = playerInventory.backpackItems[slotIndex];
                break;
            case InventoryType.Primary:
                targetItem = playerInventory.primaryItems[slotIndex];
                break;
            case InventoryType.PrimaryWeapon:
                targetItem = playerInventory.primaryWeapon;
                break;
            default:
                break;
        }

        if (targetItem.amount <= 1)
        {
            DropItem(currentItemSlot, inventoryType, slotIndex);
        }
        else
        {
            targetItem.DecreaseAmount();
            UIInventory.SetChildItemAmount(currentItemSlot, targetItem.amount);
        }

        UIInventory.DisplayActiveSlot(inventoryMenu.currentAttachmentObject, false);
        return targetItem;
    }

    public Item DropItem(GameObject currentItemSlot, InventoryType inventoryType, int slotIndex)
    {
        Item droppedItem = null;

        switch (inventoryType)
        {
            case InventoryType.Backpack:
                droppedItem = playerInventory.RemoveBackpackItem(slotIndex);
                break;
            case InventoryType.Primary:
                droppedItem = playerInventory.RemovePrimaryItem(slotIndex);
                break;
            case InventoryType.PrimaryWeapon:
                droppedItem = playerInventory.RemovePrimaryWeapon();
                visualWeaponObject.SetSprite(null);
                visualWeaponObject.PlayDropSound();
                break;
            default:
                break;
        }

        UIInventory.DisplayActiveSlot(currentItemSlot, false);
        UIInventory.RemoveInventoryItem(currentItemSlot);

        UIInventory.DisplayActiveSlot(inventoryMenu.currentAttachmentObject, false);
        return droppedItem;
    }

    public void MoveToPrimaryWeapon(GameObject currentItemSlot, InventoryType inventoryType, int slotIndex)
    {
        InterchangeItems(currentItemSlot, UIInventory.PrimaryWeapon, inventoryType, slotIndex, InventoryType.PrimaryWeapon, 0);
        visualWeaponObject.SetSprite(playerInventory.primaryWeapon.sprite);

        UIInventory.DisplayActiveSlot(inventoryMenu.currentAttachmentObject, false);
    }
}
