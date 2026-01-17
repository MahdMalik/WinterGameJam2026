using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterDoors : Perk
{
    public FasterDoors(int cost, Perk prev, Vector2 pos, bool upgradedVer) : base("FasterDoors", "Increase door opening speed by 15%", cost, prev, pos, upgradedVer)
    {
    }

    
    protected override void ActivatePerk()
    {
        Initializer.doorOpeningSpeed *= 0.5f;
    }
}
