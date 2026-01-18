using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreInvincibility : Perk
{
    public MoreInvincibility(int cost, Perk prev, Vector2 pos, bool upgradedVer) : base("MoreInvincibility", "DOUBLE INVINCIBILITY TIME AFTER AN ATTACK", cost, prev, pos, upgradedVer, "MORE INVINCIBILITY")
    {
    }

    
    protected override void ActivatePerk()
    {
        Initializer.timeCounterForInvincibility *= 2;
    }
}
