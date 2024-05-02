using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpForce;
    public float speed;
    public Vector2 groundCheckOffset; //checking for ground offset position
    private bool isGrounded;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int jumpCount = 0;

    private Animator anim;
    private float horizontalMovement;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if the player is grounded
        Vector2 groundCheckPos = rb.position + groundCheckOffset;
        isGrounded = Physics2D.OverlapCircle(groundCheckPos, checkRadius, whatIsGround);

        // Reset jump count if grounded
        if (isGrounded && rb.velocity.y <= 0) 
        {
            jumpCount = 0;
            anim.SetBool("isJumping", false);
        }

        // Apply horizontal movement
        rb.velocity = new Vector2(horizontalMovement * speed, rb.velocity.y);
        anim.SetBool("isRunning", horizontalMovement != 0);

        // Handle sprite flipping
        if (horizontalMovement < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (horizontalMovement > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        // Only jump if the jump button is pressed, we're either grounded or have only jumped once
        if (context.phase == InputActionPhase.Started && jumpCount < 2)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
            anim.SetBool("isJumping", true);
        }
    }

    public void onAbility(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            anim.SetTrigger("abilityTrigger");
        }
    }
}
