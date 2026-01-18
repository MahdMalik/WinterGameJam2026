using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterWeapons : Perk
{
    public FasterWeapons(int cost, Perk prev, Vector2 pos, bool upgradedVer) : base("FasterWeapons", "SPEEDS UP WEAPON USAGE BY 20%", cost, prev, pos, upgradedVer, "FASTER WEAPONS")
    {
    }

    
    protected override void ActivatePerk()
    {
        Initializer.useSpeedMultiplier *= 0.8f;
        Initializer.cooldownMultiplier *= 0.8f;
    }
}
