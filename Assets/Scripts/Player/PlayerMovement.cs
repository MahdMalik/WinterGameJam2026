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
    [SerializeField] private SpriteRenderer PlayerSprite;
    [SerializeField] Animator playerAnim;
    [SerializeField] GameObject SceneManagement = null;

    // make sure that when the battery dies out, we restart the game (for now; normally
    // there'd be a game over screen)
    void Start()
    {
        SceneManagement = GameObject.Find("SceneManager");
        Battery.OnPlayerDied += ResetPlayer;
    }

    void FixedUpdate()
    {
        //Movement
        if (Initializer.worldFrozen == false) {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Time.deltaTime * PlayerSpeed, Input.GetAxisRaw("Vertical") * Time.deltaTime * PlayerSpeed);
        } else {
            rb.velocity = new Vector2(0.0f, 0.0f);
        }
        }


    void Update() {
        OpeningDoor = false;
        //Interaction. Checks interact hitbox in front of player. Then activates functions based on what is there.
        if(Input.GetKeyDown(KeyCode.X) && interactHitbox.IsTouchingLayers(LayerMask.GetMask("Door"))) 
        {
            OpeningDoor = true;
        }

        //Check if walking
        if (rb.velocity.x == 0 && rb.velocity.y == 0) {
            playerAnim.SetBool("Walking", false);
            Initializer.playerMoving = false;
        } 
        else if (Initializer.worldFrozen == false)
        {
            playerAnim.SetBool("Walking", true);
            Initializer.playerMoving = true;
            if(PlayerVars.canTurnInteract)
            {
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

                if(PlayerVars.PlayerFacing == 1) {
                    PlayerSprite.sprite = Up;
                    interactSquare.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
                } else if(PlayerVars.PlayerFacing == 2) {
                    PlayerSprite.sprite = Right;
                    interactSquare.transform.position = new Vector3(transform.position.x + 0.8f, transform.position.y + 0.5f, transform.position.z);
                } else if(PlayerVars.PlayerFacing == 3) {
                    PlayerSprite.sprite = Down;
                    interactSquare.transform.position = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
                } else if(PlayerVars.PlayerFacing == 4) {
                    PlayerSprite.sprite = Left;
                    interactSquare.transform.position = new Vector3(transform.position.x - 0.8f, transform.position.y + 0.5f, transform.position.z);
                }
            }

        }
    }

    // resets the player when a new run starts
    void ResetPlayer()
    {
        StartCoroutine(DeathAnim());
    }
    IEnumerator DeathAnim() {
        playerAnim.SetTrigger("Dead");
        Initializer.worldFrozen = true;
        yield return new WaitForSeconds(1.0f);
        SceneManagement.GetComponent<SceneManagerer>().Next();
    }

    void OnDestroy()
    {
        Battery.OnPlayerDied -= ResetPlayer;
    }
}