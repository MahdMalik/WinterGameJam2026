using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSpeedBoost : Perk
{
    public WalkSpeedBoost(int cost, Perk prev, Vector2 pos, bool upgradedVer) : base("WalkSpeedBoost", "INCREASE WALKING SPEED BY 15%", cost, prev, pos, upgradedVer, "WALK SPEED BOOST")
    {
    }

    
    protected override void ActivatePerk()
    {
        Initializer.playerSpeed *= 1.25f;
    }
}
