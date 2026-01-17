using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperFunctions
{
    public static Vector3 PutDistanceAway(Vector3 playerPos, float multiplierOffset)
    {
        float xOffset = Initializer.PlayerFacing == 2 ? 1 : Initializer.PlayerFacing == 4 ? - 1 : 0; 
        float yOffset = Initializer.PlayerFacing == 1 ? 1 : Initializer.PlayerFacing == 3 ? -1 : 0;
        return new Vector3(playerPos.x + xOffset * multiplierOffset, playerPos.y + yOffset * multiplierOffset);
    }
}
