using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Coin : Item
{
    public ItemType typeOfItem { get; set; } = ItemType.Coin;
    public Sprite sprite { get; set; } = ItemSprites.instance.coin;
    public int amount { get; set; } = 1;

    public Coin WithAmount(int amount)
    {
        this.amount = amount;
        return this;
    }

    public int IncreaseAmount()
    {
        amount++;
        return amount;
    }

    public int DecreaseAmount()
    {
        amount--;
        return amount;
    }

    public void Use()
    {

    }
}
