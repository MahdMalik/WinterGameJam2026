using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreDamage : Perk
{
    public MoreDamage(int cost, Perk prev, Vector2 pos, bool upgradedVer) : base("MoreDamage", "INCREASES DAMAGE 1.5X.", cost, prev, pos, upgradedVer, "MORE DAMAGE")
    {
    }

    
    protected override void ActivatePerk()
    {
        Initializer.playerDamage *= 1.5f;
    }
}
