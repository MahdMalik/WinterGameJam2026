using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightsaber : Item
{
    // need these animation stuff to alter length to what we want to be
    [SerializeField] Animator slashAnim;
    public AnimationClip swordSlashClip;

    private float originalClipTime;

    // gets the origianl clip time in seconds, divides that by activation time to see how much to change
    //ratio of playback speed by. 
    protected override void Start()
    {
        originalClipTime = swordSlashClip.length;
        
        slashAnim.speed = originalClipTime / activationTime;

        base.Start();
    }

    // updates the position of the slash each frame to be a bit in front of the player, only if the salsh is activated
    // of course.
    protected override void Update()
    {
        if(activated)
        {
            float xOffset = PlayerVars.PlayerFacing == 2 ? 1 : PlayerVars.PlayerFacing == 4 ? - 1 : 0; 
            float yOffset = PlayerVars.PlayerFacing == 1 ? 1 : PlayerVars.PlayerFacing == 3 ? -1 : 0;
            transform.position = new Vector3(player.transform.position.x + xOffset * 1.2f, player.transform.position.y + yOffset * 1.2f);
        }

        base.Update();
    }

    // When we first acitvate it, have to set these slash directions, then we can trigger the slashing.
    public override void Activate()
    {
        if (PlayerVars.PlayerFacing == 1) {
            slashAnim.SetInteger("SlashDirection", 1);
        } else if (PlayerVars.PlayerFacing == 2) {
            slashAnim.SetInteger("SlashDirection", 2);
        } else if (PlayerVars.PlayerFacing == 3) {
            slashAnim.SetInteger("SlashDirection", 3);
        } else {
            slashAnim.SetInteger("SlashDirection", 4);
        }
        slashAnim.SetTrigger("Slashing");        
        base.Activate();
    }
}
