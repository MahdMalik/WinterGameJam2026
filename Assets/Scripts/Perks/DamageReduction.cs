using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReduction : Perk
{
    public DamageReduction(int cost, Perk prev, Vector2 pos, bool upgradedVer) : base("DamageReduction", "Reduced Battery Damage On Hit by 50%", cost, prev, pos, upgradedVer, "Damage Reduction")
    {
    }

    
    protected override void ActivatePerk()
    {
        Initializer.damageInSecTaken /= 2;
    }
}
