using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReduction : Perk
{
    public DamageReduction(int cost, Perk prev, Vector2 pos, bool upgradedVer) : base("DamageReduction", "REDUCED BATTERY DAMAGE ON HIT BY 50%", cost, prev, pos, upgradedVer, "DAMAGE REDUCTION")
    {
    }

    
    protected override void ActivatePerk()
    {
        Initializer.damageInSecTaken /= 2;
    }
}
