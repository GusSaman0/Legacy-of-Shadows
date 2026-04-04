using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movimiento
    public float speed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;

    // Salto
    public float jumpForce = 8f;
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;

    public bool isGrounded;
    bool jumpRequest;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movimiento horizontal
        movement.x = Input.GetAxisRaw("Horizontal");

        // Detectar suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        // Detectar salto
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            jumpRequest = true;
        }

        // Voltear personaje
        if (movement.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void FixedUpdate()
    {
        // Movimiento
        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);

        // Salto
        if (jumpRequest)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpRequest = false;
        }
    }
}