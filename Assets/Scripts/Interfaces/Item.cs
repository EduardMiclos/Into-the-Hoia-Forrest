using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Item
{
    ItemType typeOfItem {get; set;}
    Sprite sprite {get; set;}
    int amount {get; set;}

    public int IncreaseAmount();
    public int DecreaseAmount();
    public void Use();
}
