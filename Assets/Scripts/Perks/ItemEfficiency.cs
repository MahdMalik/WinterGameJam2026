using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEfficiency : Perk
{
    public ItemEfficiency(int cost, Perk prev, Vector2 pos, bool upgradedVer) : base("ItemEfficiency", "NO LONGER USES ENERGY ON ITEM ACTIVATION", cost, prev, pos, upgradedVer, "ITEM EFFICIENCY")
    {
    }

    
    protected override void ActivatePerk()
    {
        Initializer.itemsUsePower = false;
    }
}
