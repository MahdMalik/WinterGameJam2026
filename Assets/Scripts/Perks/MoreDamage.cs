using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreDamage : Perk
{
    public MoreDamage(int cost, Perk prev, Vector2 pos, bool upgradedVer) : base("MoreDamage", "Increases damage by 1.5x.", cost, prev, pos, upgradedVer, "More Damage")
    {
    }

    
    protected override void ActivatePerk()
    {
        Initializer.playerDamage *= 1.5f;
    }
}
