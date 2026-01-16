using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int directionShot;

    private float bulletSpeed;
    public void Activate(Vector3 playerPos, float theBulletSpeed)
    {
        directionShot = PlayerVars.PlayerFacing;
        bulletSpeed = theBulletSpeed;
        transform.position = HelperFunctions.PutDistanceAway(playerPos, 1.3f);
    }

    public void UpdateBullet()
    {
        float xOffset = bulletSpeed * (directionShot == 2 ? 1 : directionShot == 4 ? -1 : 0);
        float yOffset = bulletSpeed * (directionShot == 1 ? 1 : directionShot == 3 ? -1 : 0);
        transform.position = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, transform.position.z);
    }
}
