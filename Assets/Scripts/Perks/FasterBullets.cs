using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterBullets : Perk
{
    public FasterBullets(int cost, Perk prev, Vector2 pos, bool upgradedVer) : base("FasterBullets", "Doubles the speed of bullets.", cost, prev, pos, upgradedVer, "Faster Bullets")
    {
    }

    
    protected override void ActivatePerk()
    {
        Initializer.playerDamage *= 1.5f;
    }
}
