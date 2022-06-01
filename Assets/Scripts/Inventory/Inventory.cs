using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class Inventory : MonoBehaviour
{
    public List<Item> backpackItems { get; set; }
    public SortedSet<int> freeBackpackSlots { get; set; }

    public List<Item> primaryItems { get; set; }
    public SortedSet<int> freePrimarySlots { get; set; }

    public Item primaryWeapon { get; set; }

    public int backpackCapacity { get; set; } = 9;
    public int primaryCapacity { get; set; } = 2;

    public Inventory()
    {
        backpackItems = new List<Item>(new Item[backpackCapacity]);
        freeBackpackSlots = new SortedSet<int>(Enumerable.Range(0, backpackCapacity));

        primaryItems = new List<Item>(new Item[primaryCapacity]);
        freePrimarySlots = new SortedSet<int>(Enumerable.Range(0, primaryCapacity));

        primaryWeapon = null;
    }

    internal int AddItem(Item item, List<Item> items, SortedSet<int> freeItemSlots)
    {
        try
        {
            int freeSlot = freeItemSlots.Min();
            freeItemSlots.Remove(freeSlot);

            items[freeSlot] = item;
            return freeSlot;
        }
        catch (InvalidOperationException)
        {
            return -1;
        }
    }

    internal Item RemoveItem(List<Item> items, SortedSet<int> freeItemSlots, int slotIndex)
    {
        try
        {
            Item removedItem = items[slotIndex];
            items[slotIndex] = null;
            freeItemSlots.Add(slotIndex);

            return removedItem;
        }
        catch (ArgumentOutOfRangeException)
        {
            return null;
        }
    }

    internal int AddBackpackItem(Item item)
    {
        return AddItem(item, backpackItems, freeBackpackSlots);
    }

    internal Item RemoveBackpackItem(int slotIndex)
    {
        return RemoveItem(backpackItems, freeBackpackSlots, slotIndex);
    }

    internal int AddPrimaryItem(Item item)
    {
        return AddItem(item, primaryItems, freePrimarySlots);
    }

    internal Item RemovePrimaryItem(int slotIndex)
    {
        return RemoveItem(primaryItems, freePrimarySlots, slotIndex);
    }

    internal void AddPrimaryWeapon(Weapon weapon)
    {
        primaryWeapon = weapon;
    }

    internal Item RemovePrimaryWeapon()
    {
        Item removedWeapon = primaryWeapon;
        primaryWeapon = null;

        return removedWeapon;
    }


    internal bool InterchangePrimaryWeaponWithItem(InventoryType inventoryType, int slotIndex)
    {
        Item auxItem = null;

        switch(inventoryType)
        {
            case InventoryType.Backpack:
                auxItem = backpackItems[slotIndex];

                if (auxItem != null && auxItem.typeOfItem != ItemType.Weapon)
                {
                    return false;
                }

                if (primaryWeapon == null && auxItem != null)
                {
                    freeBackpackSlots.Add(slotIndex);
                }

                backpackItems[slotIndex] = primaryWeapon;
                primaryWeapon = auxItem;
                break;
            case InventoryType.Primary:
                auxItem = primaryItems[slotIndex];

                if (auxItem != null && auxItem.typeOfItem != ItemType.Weapon)
                {
                    return false;
                }

                if (primaryWeapon == null && auxItem != null)
                {
                    freePrimarySlots.Add(slotIndex);
                }

                primaryItems[slotIndex] = primaryWeapon;
                primaryWeapon = auxItem;
                break;
            default:
                break;
        }


        return auxItem == null || auxItem.typeOfItem == ItemType.Weapon;
    }

    private void SelectInventory(InventoryType inventoryType, out List<Item> itemList, out SortedSet<int> freeSlots)
    {
        switch (inventoryType)
        {
            case InventoryType.Backpack:
                itemList = backpackItems;
                freeSlots = freeBackpackSlots;
                break;
            case InventoryType.Primary:
                itemList = primaryItems;
                freeSlots = freePrimarySlots;
                break;
            default:
                itemList = null;
                freeSlots = null;
                break;
        }
    }

    private void ConfigureInventories(Item item, ref SortedSet<int> freeSlots, int slotIndex)
    {
        if (item == null)
        {
            freeSlots.Add(slotIndex);
        }
        else
        {
            freeSlots.Remove(slotIndex);
        }
    }

    private void UpdateInventory(InventoryType inventoryType, List <Item> items, SortedSet<int> freeSlots)
    {
        switch (inventoryType)
        {
            case InventoryType.Backpack:
                backpackItems = items;
                freeBackpackSlots = freeSlots;
                break;
            case InventoryType.Primary:
                primaryItems = items;
                freePrimarySlots = freeSlots;
                break;
            default:
                break;
        }
    }

    internal bool InterchangeItemWithItem(InventoryType inventory1, int slotIndex1, InventoryType inventory2, int slotIndex2)
    {
        List<Item> items1, items2;
        SortedSet<int> freeSlotsItems1, freeSlotsItems2;

        SelectInventory(inventory1, out items1, out freeSlotsItems1);
        SelectInventory(inventory2, out items2, out freeSlotsItems2);
        
        ConfigureInventories(items1[slotIndex1], ref freeSlotsItems1, slotIndex2);
        ConfigureInventories(items2[slotIndex2], ref freeSlotsItems2, slotIndex1);

        Item auxItem = null;
        auxItem = items1[slotIndex1];
        items1[slotIndex1] = items2[slotIndex2];
        items2[slotIndex2] = auxItem;

        UpdateInventory(inventory1, items1, freeSlotsItems1);
        UpdateInventory(inventory2, items2, freeSlotsItems2);

        return true;

    }

    internal bool InterchangeItems(InventoryType inventory1, int slotIndex1, InventoryType inventory2, int slotIndex2)
    {

        if (inventory1 == InventoryType.PrimaryWeapon)
        {
            return InterchangePrimaryWeaponWithItem(inventory2, slotIndex2);
        }

        if (inventory2 == InventoryType.PrimaryWeapon)
        {
            return InterchangePrimaryWeaponWithItem(inventory1, slotIndex1);
        }

        return InterchangeItemWithItem(inventory1, slotIndex1, inventory2, slotIndex2);
    }
}
