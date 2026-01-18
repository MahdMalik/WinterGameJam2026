using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBattery : Perk
{
    public ExtraBattery(int cost, Perk prev, Vector2 pos, bool upgradedVer) : base("ExtraBattery", "ADDS 20 SECONDS OF BATTERY TIME.", cost, prev, pos, upgradedVer, "EXTRA BATTERY")
    {
    }

    
    protected override void ActivatePerk()
    {
        Initializer.numSecondsFromMax += 20;
    }
}
