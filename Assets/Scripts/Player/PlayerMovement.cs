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
    [SerializeField] private float objectUseSpeed;
    [SerializeField] private SpriteRenderer PlayerSprite;
    [SerializeField] Animator playerAnim;
    [SerializeField] GameObject SceneManagement = null;

    private float lastUpdate;

    private Sprite[] movementSprites;
    // make sure that when the battery dies out, we restart the game (for now; normally
    // there'd be a game over screen)
    void Start()
    {
        SceneManagement = GameObject.Find("SceneManager");
        Battery.OnPlayerDied += ResetPlayer;
        movementSprites = new Sprite[] {Up, Right, Down, Left};
        lastUpdate = Time.time;

        Initializer.secondsSurvived = 0;
        Initializer.numKillsThisRound = 0;
        Initializer.maxDistFromCenter = 0;
        Initializer.pointsLastRun = 0;
    }

    void FixedUpdate()
    {
        //Movement
        if (Initializer.worldFrozen == false) {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Time.deltaTime * Initializer.playerSpeed, Input.GetAxisRaw("Vertical") * Time.deltaTime * Initializer.playerSpeed);
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
            if(Initializer.canTurnInteract)
            {
                //Facing Logic
                if(rb.velocity.x == 0 && rb.velocity.y > 0) {
                    Initializer.PlayerFacing = 1;
                    interactSquare.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
                }//Up
                if (rb.velocity.x > 0) {
                    Initializer.PlayerFacing = 2;
                    interactSquare.transform.position = new Vector3(transform.position.x + 0.8f, transform.position.y + 0.5f, transform.position.z);
                }//Right
                if (rb.velocity.x == 0 && rb.velocity.y < 0) {
                    Initializer.PlayerFacing = 3;
                    interactSquare.transform.position = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
                }//Down
                if (rb.velocity.x < 0) {
                    Initializer.PlayerFacing = 4;
                    interactSquare.transform.position = new Vector3(transform.position.x - 0.8f, transform.position.y + 0.5f, transform.position.z);
                }//Left

                playerAnim.SetInteger("WalkingDirection", Initializer.PlayerFacing);
                PlayerSprite.sprite = movementSprites[Initializer.PlayerFacing - 1];
            }
        }
        float distFromCenter = (float) Math.Sqrt( Math.Pow(Math.Abs(transform.position.x), 2) + Math.Pow(Math.Abs(transform.position.y), 2));
        if(distFromCenter > Initializer.maxDistFromCenter)
        {
            // Debug.Log($"They went farther! At dist {distFromCenter}");
            Initializer.maxDistFromCenter = distFromCenter;
        }

        // update every secohnd their time survived 
        if(lastUpdate > 1)
        {
            Initializer.secondsSurvived += 1;
            lastUpdate = 0;
        }
        lastUpdate += Time.deltaTime;
    }

    // resets the player when a new run starts
    void ResetPlayer()
    {
        // Points system: Math.floor[ (time survived / 60) + (numEnemiesKilled / 2) + (maxDistFromCenter / 100) ] 
        Debug.Log(Initializer.secondsSurvived / 60.0f);
        Debug.Log(Initializer.numKillsThisRound / 2.0f);
        Debug.Log(Initializer.maxDistFromCenter / 1000.0f);
        Initializer.pointsLastRun = (int) Math.Ceiling(
          Initializer.secondsSurvived / 60.0f  +
          Initializer.numKillsThisRound / 2.0f +
          Initializer.maxDistFromCenter / 1000.0f
        );
        Initializer.perkPoints += Initializer.pointsLastRun;
        
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