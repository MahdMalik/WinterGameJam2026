using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidingArrow : Perk
{
    public GuidingArrow(int cost, Perk prev, Vector2 pos, bool upgradedVer) : base("GuidingArrow", "Reveals An Arrow (At Start) Towards The Exist", cost, prev, pos, upgradedVer, "Guided Arrow")
    {
    }

    
    protected override void ActivatePerk()
    {
        Initializer.guidingArrow = true;
    }
}
