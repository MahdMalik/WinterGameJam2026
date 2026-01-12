using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Sprite Up;
    [SerializeField] Sprite Right;
    [SerializeField] Sprite Down;
    [SerializeField] Sprite Left;
    [SerializeField] public bool OpeningDoor;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject interactSquare;
    [SerializeField] private BoxCollider2D interactHitbox;
    [SerializeField] private float PlayerSpeed;
    [SerializeField] private float objectUseSpeed;
    [SerializeField] private bool canAct = true;
    [SerializeField] private bool canTurnInteract = true;
    [SerializeField] private SpriteRenderer PlayerSprite;
    [SerializeField] Animator playerAnim;
    [SerializeField] Animator slashAnim;

    public static Action ResetGame;

    void Start()
    {

        Battery.OnPlayerDied += ResetPlayer;
    }

    void FixedUpdate()
    {
        //Movement
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Time.deltaTime * PlayerSpeed, Input.GetAxisRaw("Vertical") * Time.deltaTime * PlayerSpeed);
        }


    void Update() {
        OpeningDoor = false;
        //Interaction. Checks interact hitbox in front of player. Then activates functions based on what is there.
        if(Input.GetKeyDown(KeyCode.Z)) {
            if (interactHitbox.IsTouchingLayers(LayerMask.GetMask("Door"))) {
                OpeningDoor = true;
            } else  if (canAct) {
            canAct = false;
            canTurnInteract = false;
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
            StartCoroutine(WaitToAct());
        }
        }

        //Check if walking
        if (rb.velocity.x == 0 && rb.velocity.y == 0) {
            playerAnim.SetBool("Walking", false);
        } else {
            playerAnim.SetBool("Walking", true);
        
        //Facing Logic
        if(rb.velocity.x == 0 && rb.velocity.y > 0) {
            PlayerVars.PlayerFacing = 1;
            playerAnim.SetInteger("WalkingDirection", 1);
        }//Up
        if (rb.velocity.x > 0) {
            PlayerVars.PlayerFacing = 2;
            playerAnim.SetInteger("WalkingDirection", 2);
        }//Right
        if (rb.velocity.x == 0 && rb.velocity.y < 0) {
            PlayerVars.PlayerFacing = 3;
            playerAnim.SetInteger("WalkingDirection", 3);
        }//Down
        if (rb.velocity.x < 0) {
            PlayerVars.PlayerFacing = 4;
            playerAnim.SetInteger("WalkingDirection", 4);
        }//Left
        }
        if(PlayerVars.PlayerFacing == 1 && canTurnInteract) {
            PlayerSprite.sprite = Up;
            interactSquare.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
        } else if(PlayerVars.PlayerFacing == 2 && canTurnInteract) {
            PlayerSprite.sprite = Right;
            interactSquare.transform.position = new Vector3(transform.position.x + 0.8f, transform.position.y + 0.5f, transform.position.z);
        } else if(PlayerVars.PlayerFacing == 3 && canTurnInteract) {
            PlayerSprite.sprite = Down;
            interactSquare.transform.position = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
        } else if(PlayerVars.PlayerFacing == 4 && canTurnInteract) {
            PlayerSprite.sprite = Left;
            interactSquare.transform.position = new Vector3(transform.position.x - 0.8f, transform.position.y + 0.5f, transform.position.z);
        }
    }

    void ResetPlayer()
    {
        transform.position = Vector3.zero;
        rb.velocity = Vector2.zero;
        ResetGame.Invoke();
    }


    IEnumerator WaitToAct() {
        yield return new WaitForSeconds(objectUseSpeed/2);
        canTurnInteract = true;
        yield return new WaitForSeconds(objectUseSpeed/2);
        canAct = true;
    }


}