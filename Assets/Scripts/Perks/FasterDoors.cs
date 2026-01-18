using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterDoors : Perk
{
    public FasterDoors(int cost, Perk prev, Vector2 pos, bool upgradedVer) : base("FasterDoors", "INCREASE DOOR OPENING SPEED BY 15%", cost, prev, pos, upgradedVer, "FASTER DOORS")
    {
    }

    
    protected override void ActivatePerk()
    {
        Initializer.doorOpeningSpeed *= 0.5f;
    }
}
