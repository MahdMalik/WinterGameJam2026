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
}