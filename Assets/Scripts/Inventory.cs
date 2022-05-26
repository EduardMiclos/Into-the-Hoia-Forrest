using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
   private List<Item> backpackItems;
   private List<Item> primaryItems;
   private Item primaryWeapon;
   
   private int backpackCapacity = 6;
   private int primaryCapacity = 2;

    public Inventory()
    {
        backpackItems = new List<Item>();
        primaryItems = new List<Item>();
    }

    internal bool AddItem(Item item)
    {
        return false;
    }
}
