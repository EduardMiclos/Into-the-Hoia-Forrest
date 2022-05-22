using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private ItemType itemType;
    private Sprite itemImage;

    private int count = 0;

    public void increaseAmount()
    {
        count++;
    }

    public void decreaseAmount()
    {
        count--;
    }
}
