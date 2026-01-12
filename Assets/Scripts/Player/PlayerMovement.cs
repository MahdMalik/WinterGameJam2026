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
    [SerializeField] private GameObject Player;
    [SerializeField] private float PlayerSpeed;
    [SerializeField] private int PlayerFacing = 3;
    [SerializeField] private SpriteRenderer PlayerSprite;
    [SerializeField] Animator playerAnim;

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
            }
        }

                //Check if walking
        if (rb.velocity.x == 0 && rb.velocity.y == 0) {
            playerAnim.SetBool("Walking", false);
        } else {
            playerAnim.SetBool("Walking", true);
        //Facing Logic
        if(rb.velocity.x == 0 && rb.velocity.y > 0) {
            PlayerFacing = 1;
            playerAnim.SetInteger("WalkingDirection", 1);
        }//Up
        if (rb.velocity.x > 0) {
            PlayerFacing = 2;
            playerAnim.SetInteger("WalkingDirection", 2);
        }//Right
        if (rb.velocity.x == 0 && rb.velocity.y < 0) {
            PlayerFacing = 3;
            playerAnim.SetInteger("WalkingDirection", 3);
        }//Down
        if (rb.velocity.x < 0) {
            PlayerFacing = 4;
            playerAnim.SetInteger("WalkingDirection", 4);
        }//Left
        }
        if(PlayerFacing == 1) {
            PlayerSprite.sprite = Up;
            interactSquare.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 1.0f, Player.transform.position.z);
        } else if(PlayerFacing == 2) {
            PlayerSprite.sprite = Right;
            interactSquare.transform.position = new Vector3(Player.transform.position.x + 1.0f, Player.transform.position.y, Player.transform.position.z);
        } else if(PlayerFacing == 3) {
            PlayerSprite.sprite = Down;
            interactSquare.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y - 1.0f, Player.transform.position.z);
        } else {
            PlayerSprite.sprite = Left;
            interactSquare.transform.position = new Vector3(Player.transform.position.x - 0.3f, Player.transform.position.y, Player.transform.position.z);
        }
    }

    void ResetPlayer()
    {
        Player.transform.position = Vector3.zero;
        rb.velocity = Vector2.zero;
        ResetGame.Invoke();
    }
}