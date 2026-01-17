using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightsaber : Item
{
    // need these animation stuff to alter length to what we want to be
    [SerializeField] Animator slashAnim;
    public AnimationClip swordSlashClip;
    [SerializeField] private float energyUse;

    private float originalClipTime;

    // gets the origianl clip time in seconds, divides that by activation time to see how much to change
    //ratio of playback speed by. 
    protected override void Start()
    {
        originalClipTime = swordSlashClip.length;
        
        slashAnim.speed = originalClipTime / activationTime;

        base.Start();

        // set slash to current position
        transform.GetChild(0).localPosition = new Vector3(0, 0, 0);
    }

    // updates the position of the slash each frame to be a bit in front of the player, only if the salsh is activated
    // of course.
    protected override void Update()
    {
        if(activated)
        {
            transform.position = HelperFunctions.PutDistanceAway(player.transform.position, 1.2f);
        }

        base.Update();
    }

    public override void OnEndActivation()
    {
        Initializer.canTurnInteract = true;
        base.OnEndActivation();
    }

    // When we first acitvate it, have to set these slash directions, then we can trigger the slashing.
    public override void Activate()
    {
        slashAnim.SetInteger("SlashDirection", Initializer.PlayerFacing);
        
        slashAnim.SetTrigger("Slashing");    
        Initializer.canTurnInteract = false;    
        base.Activate();
        Initializer.batteryPower -= energyUse;
    }
}
