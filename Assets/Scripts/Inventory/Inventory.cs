using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class Inventory : MonoBehaviour
{
    public List<Item> backpackItems {get; set;}
    public SortedSet<int> freeBackpackSlots { get; set; }

    public List<Item> primaryItems {get; set;}
    public SortedSet<int> freePrimarySlots { get; set; }

    public Item primaryWeapon {get; set;}
   
    public int backpackCapacity {get; set;} = 9;
    public int primaryCapacity {get; set;} = 2;   

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

    internal int AddBackpackItem(Item item)
    {
        return AddItem(item, backpackItems, freeBackpackSlots);
    }

    internal int AddPrimaryItem(Item item)
    {
        return AddItem(item, primaryItems, freePrimarySlots);
    }

    internal void AddPrimaryWeapon(Weapon weapon)
    {
        primaryWeapon = weapon;
    }
}
