using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestGuardian : NPC
{
    public Player player;
    internal override void OnDialogEnd() 
    {
        player.weapon.Upgrade();
    }
}
