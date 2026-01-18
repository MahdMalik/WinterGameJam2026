using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidingArrow : Perk
{
    public GuidingArrow(int cost, Perk prev, Vector2 pos, bool upgradedVer) : base("GuidingArrow", "REVEALS AN ARROW (AT THE START) TOWARDS THE EXIT", cost, prev, pos, upgradedVer, "GUIDED ARROW")
    {
    }

    
    protected override void ActivatePerk()
    {
        Initializer.guidingArrow = true;
    }
}
