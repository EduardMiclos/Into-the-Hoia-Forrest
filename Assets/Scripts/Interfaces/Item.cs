using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Item
{
    ItemType typeOfItem {get; set;}
    SpriteRenderer sprite {get; set;}
    int amount {get; set;}

    public void increaseAmount();
    public void decreaseAmount();
}
