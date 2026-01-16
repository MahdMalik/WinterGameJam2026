using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlaster : Item
{
    public Bullet theBullet;
    
    // gets the origianl clip time in seconds, divides that by activation time to see how much to change
    //ratio of playback speed by. 
    protected override void Start()
    {
        base.Start();
        
        // set bullet to current position
        theBullet.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        // bullet.GetComponent<SpriteRenderer>().enabled = true;
        theBullet.gameObject.SetActive(false);
    }

    // updates the position of the slash each frame to be a bit in front of the player, only if the salsh is activated
    // of course.
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnEndActivation()
    {
        theBullet.gameObject.SetActive(false);
        base.OnEndActivation();
    }

    // When we first acitvate it, have to set these slash directions, then we can trigger the slashing.
    public override void Activate()
    {
        theBullet.gameObject.SetActive(true);
        Debug.Log("grok is this true?");

        
        base.Activate();
    }
}
