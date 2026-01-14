using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : Item
{
    public Battery theBattery;

    public float batteryAddCount;

    // gets the origianl clip time in seconds, divides that by activation time to see how much to change
    //ratio of playback speed by. 
    protected override void Start()
    {
        base.Start();
    }

    // updates the position of the slash each frame to be a bit in front of the player, only if the salsh is activated
    // of course.
    protected override void Update()
    {
        base.Update();
    }

    // When we first acitvate it, have to set these slash directions, then we can trigger the slashing.
    public override void Activate()
    {
        theBattery.RestoreBattery(batteryAddCount);
        base.Activate();
    }
}
