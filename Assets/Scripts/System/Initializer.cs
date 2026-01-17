using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Initializer
{
    public static RenderTexture RT;
    public static GameObject PixelatedPanel;
    public static Camera PixelCamera;
    public static float batteryPower;
    public static int maxBattery = 100;
    public static int numSecondsFromMax = 5;
    public static float SFXVolume;
    public static bool worldFrozen;
    public static bool playerMoving;
    public static int perkPoints = 0;

    public static int pointsLastRun = 0;

    public static int PlayerFacing = 3;
    public static bool canTurnInteract = true;
    public static int numKillsThisRound = 0;
    public static float maxDistFromCenter = 0;
    public static int secondsSurvived = 0;

    public static float playerSpeed = 200f;


    public static Perk[] LoadPerks()
    {
        WalkSpeedBoost perk1 = new WalkSpeedBoost(2, null, new Vector2(-73, 125), false);
        return new Perk[] {perk1};
    }

    public static Perk[] perks = LoadPerks();


}