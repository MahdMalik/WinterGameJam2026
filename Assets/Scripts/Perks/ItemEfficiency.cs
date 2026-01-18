using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEfficiency : Perk
{
    public ItemEfficiency(int cost, Perk prev, Vector2 pos, bool upgradedVer) : base("ItemEfficiency", "No Longer Uses Energy On Item Activation", cost, prev, pos, upgradedVer, "Item Efficiency")
    {
    }

    
    protected override void ActivatePerk()
    {
        Initializer.itemsUsePower = false;
    }
}
