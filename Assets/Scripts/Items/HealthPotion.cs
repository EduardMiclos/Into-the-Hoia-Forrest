using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Item
{
    public ItemType typeOfItem { get; set; } = ItemType.HealthPotion;
    public Sprite sprite { get; set; } = ItemSprites.instance.healthPoition;
    public int amount { get; set; } = 1;

    public HealthPotion WithAmount(int amount)
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
