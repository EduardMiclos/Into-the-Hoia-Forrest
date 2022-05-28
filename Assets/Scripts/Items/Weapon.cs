using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public ItemType typeOfItem {get; set;} = ItemType.Weapon;
    public Sprite sprite {get; set;} = ItemSprites.instance.weapons[0];
    public int amount {get; set;} = 1;

    public int weaponSkill {get; set;} = 0;

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
    
    public void Use() {}

    public bool Upgrade()
    {
        weaponSkill++;

        if (ItemSprites.instance.weapons.Length <= weaponSkill)
        {
            return false;
        }

        sprite = ItemSprites.instance.weapons[weaponSkill];
        return true;
    }
}
