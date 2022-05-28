using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
   public List<Item> backpackItems {get; set;}
   public List<Item> primaryItems {get; set;}
   public Item primaryWeapon {get; set;}
   
   public int backpackCapacity {get; set;} = 9;
   public int primaryCapacity {get; set;} = 2;

    public Inventory()
    {
        backpackItems = new List<Item>();
        primaryItems = new List<Item>();
        primaryWeapon = null;
    }

    internal void AddBackpackItem(Item item)
    {
        backpackItems.Add(item);
    }

    internal void AddPrimaryItem(Item item)
    {
        primaryItems.Add(item);
    }

    internal void AddPrimaryWeapon(Weapon weapon)
    {
        primaryWeapon = weapon;
    }

    /* to be deleted. */
    internal bool AddItem(Item item)
    {
        if (backpackItems.Count < backpackCapacity)
        {
            backpackItems.Add(item);
            return true;
        }

        if (primaryItems.Count < primaryCapacity)
        {
            primaryItems.Add(item);
            return true;
        }

        return false;
    }
}
