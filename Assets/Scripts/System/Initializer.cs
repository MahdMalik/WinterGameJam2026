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
    public static int numSecondsFromMax = 50;
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

    public static float doorOpeningSpeed = 1f;

    public static bool guidingArrow = false;

    public static float useSpeedMultiplier = 1f;
    public static float cooldownMultiplier = 1f;

    public static float playerDamage = 1;
    
    public static float timeCounterForInvincibility = 1.0f;

    public static int damageInSecTaken = 10;

    public static int bulletSpeed = 7;

    public static bool itemsUsePower = false;


    public static Perk[] LoadPerks()
    {
        WalkSpeedBoost perk1 = new WalkSpeedBoost(1, null, new Vector2(-73, 125), false);
        FasterDoors perk2 = new FasterDoors(1, perk1, new Vector2(194, 279), false);
        WalkSpeedBoost perk3 = new WalkSpeedBoost(1, perk2, new Vector2(461, 279), true);
        GuidingArrow perk4 = new GuidingArrow(1, perk3, new Vector2(728, 279), false);

        ExtraBattery perk5 = new ExtraBattery(1, null, new Vector2(56, 2), false);
        DamageReduction perk6 = new DamageReduction(1, perk5, new Vector2(280, 2), false);
        ExtraBattery perk7 = new ExtraBattery(1, perk6, new Vector2(504, 2), true);
        MoreInvincibility perk8 = new MoreInvincibility(1, perk7, new Vector2(728, 2), false);

        FasterWeapons perk9 = new FasterWeapons(1, null, new Vector2(-73, -121), false);
        FasterBullets perk10 = new FasterBullets(1, perk9, new Vector2(194, -275), false);
        MoreDamage perk11 = new MoreDamage(1, perk10, new Vector2(461, -275), false);
        ItemEfficiency perk12 = new ItemEfficiency(1, perk11, new Vector2(728, -275), false);

        return new Perk[] {perk1, perk2, perk3, perk4, perk5, perk6, perk7, perk8, perk9, perk10, perk11, perk12};
    }

    public static Perk[] perks = LoadPerks();


}