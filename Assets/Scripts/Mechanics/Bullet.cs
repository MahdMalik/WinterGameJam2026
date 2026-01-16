using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int directionShot;
    private Rigidbody2D rb;

    private LaserBlaster blaster;

    private float bulletSpeed;
    public void Activate(Vector3 playerPos, float theBulletSpeed, LaserBlaster theBlaster)
    {
        rb = GetComponent<Rigidbody2D>();
        directionShot = PlayerVars.PlayerFacing;
        bulletSpeed = theBulletSpeed;
        blaster = theBlaster;

        transform.position = HelperFunctions.PutDistanceAway(playerPos, 1f);

        float xVel = bulletSpeed * (directionShot == 2 ? 1 : directionShot == 4 ? -1 : 0);
        float yVel = bulletSpeed * (directionShot == 1 ? 1 : directionShot == 3 ? -1 : 0);

        rb.velocity = new Vector2(xVel, yVel);
    }

    // when the item is touched by the player
    private void OnTriggerEnter2D(Collider2D other)
    {
        // so it can get a bit... quirky if you disable the bullet when this is called
        Debug.Log($"name was {other.gameObject.name}");
        if(other.gameObject.name == "Tilemap" || (other.gameObject.name == "DoorHitbox" && other.gameObject.GetComponentInParent<Door>().GetDoorStatus() != 1))
        {
            if(blaster != null) blaster.OnEndActivation();
        }
    }
}
