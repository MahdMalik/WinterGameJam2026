using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSpeedBoost : Perk
{
    public WalkSpeedBoost(int cost, Perk prev, Vector2 pos, bool upgradedVer) : base("WalkSpeedBoost", "Increase walking speed by 15%", cost, prev, pos, upgradedVer)
    {
    }

    
    protected override void ActivatePerk()
    {
        Initializer.playerSpeed *= 2;
    }
}
