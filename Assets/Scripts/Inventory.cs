using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> items;

    internal void addItem(Item item)
    {
        items.Add(item);
    }
}
