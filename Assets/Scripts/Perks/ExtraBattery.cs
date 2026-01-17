using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBattery : Perk
{
    public ExtraBattery(int cost, Perk prev, Vector2 pos, bool upgradedVer) : base("ExtraBattery", "Adds about 30 seconds of battery time.", cost, prev, pos, upgradedVer, "Extra Battery")
    {
    }

    
    protected override void ActivatePerk()
    {
        Initializer.numSecondsFromMax += 30;
    }
}
