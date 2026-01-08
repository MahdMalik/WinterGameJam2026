using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float PlayerSpeed;

    void FixedUpdate()
    {
        rb.velocity = new Vector2((Input.GetAxisRaw("Horizontal") * Time.deltaTime * PlayerSpeed), (Input.GetAxisRaw("Vertical") * Time.deltaTime * PlayerSpeed));
    }
}
