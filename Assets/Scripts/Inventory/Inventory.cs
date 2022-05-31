using UnityEngine;
using System.Collections.Generic;
using C5;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public List<Item> backpackItems {get; set;}
    public IntervalHeap<int> freeBackpackSlots { get; set; }

    public List<Item> primaryItems {get; set;}
    public IntervalHeap<int> freePrimarySlots { get; set; }

    public Item primaryWeapon {get; set;}
   
    public int backpackCapacity {get; set;} = 9;
    public int primaryCapacity {get; set;} = 2;   

    public Inventory()
    {
        backpackItems = new List<Item>(new Item[backpackCapacity]);
        freeBackpackSlots = new IntervalHeap<int>();
        freeBackpackSlots.AddAll(Enumerable.Range(0, backpackCapacity));

        primaryItems = new List<Item>(new Item[primaryCapacity]);
        freePrimarySlots = new IntervalHeap<int>();
        freePrimarySlots.AddAll(Enumerable.Range(0, primaryCapacity));

        primaryWeapon = null;
    }

    internal int AddBackpackItem(Item item)
    {
        try
        {
            int freeSlot = freeBackpackSlots.FindMin();
            freeBackpackSlots.DeleteMin();

            backpackItems[freeSlot] = item;
            return freeSlot;
        }
        catch (NoSuchItemException)
        {
            return -1;
        }
    }

    internal int AddPrimaryItem(Item item)
    {
        try
        {
            int freeSlot = freePrimarySlots.FindMin();
            freePrimarySlots.DeleteMin();

            primaryItems[freeSlot] = item;
            return freeSlot;
        }
        catch (NoSuchItemException)
        {
            return -1;
        }
    }

    internal void AddPrimaryWeapon(Weapon weapon)
    {
        primaryWeapon = weapon;
    }
}
