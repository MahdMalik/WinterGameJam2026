using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterBullets : Perk
{
    public FasterBullets(int cost, Perk prev, Vector2 pos, bool upgradedVer) : base("FasterBullets", "DOUBLES THE SPEED OF BULLETS.", cost, prev, pos, upgradedVer, "FASTER BULLETS")
    {
    }

    
    protected override void ActivatePerk()
    {
        Initializer.playerDamage *= 1.5f;
    }
}
