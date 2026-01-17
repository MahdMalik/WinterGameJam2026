using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterWeapons : Perk
{
    public FasterWeapons(int cost, Perk prev, Vector2 pos, bool upgradedVer) : base("FasterWeapons", "Speeds Up Weapon Usage by 20%", cost, prev, pos, upgradedVer, "Faster Weapons")
    {
    }

    
    protected override void ActivatePerk()
    {
        Initializer.useSpeedMultiplier *= 0.8f;
        Initializer.cooldownMultiplier *= 0.8f;
    }
}
