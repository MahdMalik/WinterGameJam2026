using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreInvincibility : Perk
{
    public MoreInvincibility(int cost, Perk prev, Vector2 pos, bool upgradedVer) : base("MoreInvincibility", "Double invincibility time after an attack", cost, prev, pos, upgradedVer, "More Invincibility")
    {
    }

    
    protected override void ActivatePerk()
    {
        Initializer.timeCounterForInvincibility *= 2;
    }
}
