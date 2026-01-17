using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// made this helper colelction of variables multiple scripts need to access at once
public static class PlayerVars
{
    public static int PlayerFacing = 3;
    public static bool canTurnInteract = true;
    public static int numKillsThisRound = 0;
    public static float maxDistFromCenter = 0;
    public static int secondsSurvived = 0;

}