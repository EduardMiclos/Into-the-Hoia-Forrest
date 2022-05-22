using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
   private List<Item> backpackItems;
   private List<Item> primaryItems;
   private Item primaryWeapon;

    public Inventory()
    {
        backpackItems = new List<Item>();
        primaryItems = new List<Item>();
    }

    internal void AddItem(Item item)
    {
        backpackItems.Add(item);
        primaryItems.Add(item);
    }
}
