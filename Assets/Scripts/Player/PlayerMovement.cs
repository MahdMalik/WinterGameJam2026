using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Sprite Up;
    [SerializeField] Sprite Right;
    [SerializeField] Sprite Down;
    [SerializeField] Sprite Left;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float PlayerSpeed;
    [SerializeField] private int PlayerFacing = 3;
    [SerializeField] private SpriteRenderer PlayerSprite;


    void FixedUpdate()
    {
        rb.velocity = new Vector2((Input.GetAxisRaw("Horizontal") * Time.deltaTime * PlayerSpeed), (Input.GetAxisRaw("Vertical") * Time.deltaTime * PlayerSpeed));
        
        
        
        
        if(rb.velocity.x == 0 && rb.velocity.y > 0) {
            PlayerFacing = 1;
        }//Up
        if (rb.velocity.x > 0) {
            PlayerFacing = 2;
        }//Right
        if (rb.velocity.x == 0 && rb.velocity.y <= 0) {
            PlayerFacing = 3;
        }//Down
        if (rb.velocity.x < 0) {
            PlayerFacing = 4;
        }//Left
        if(PlayerFacing == 1) {
            PlayerSprite.sprite = Up;
        } else if(PlayerFacing == 2) {
            PlayerSprite.sprite = Right;
        } else if(PlayerFacing == 3) {
            PlayerSprite.sprite = Down;
        } else {
            PlayerSprite.sprite = Left;
        }
        Debug.Log(PlayerFacing);
    }
}
