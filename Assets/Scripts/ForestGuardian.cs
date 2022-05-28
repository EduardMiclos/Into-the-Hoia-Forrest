using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestGuardian : NPC
{
    internal override void OnDialogEnd() 
    {
        InventoryManager.instance.AddWeapon();
    }
}
