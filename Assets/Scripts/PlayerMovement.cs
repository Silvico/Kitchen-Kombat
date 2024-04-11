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

    //public Transform groundPos;
    public Vector2 groundCheckOffset; //checking for ground offset position
    private bool isGrounded;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    private bool doubleJump;

    private Vector2 movementInput;
    private float horizontalMovement;



    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 groundCheckPos = rb.position + groundCheckOffset;
        isGrounded = Physics2D.OverlapCircle(groundCheckPos, checkRadius, whatIsGround);

        rb.velocity = new Vector2(horizontalMovement * speed, rb.velocity.y);
        anim.SetBool("isRunning", horizontalMovement != 0);

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
        if (context.phase == InputActionPhase.Started && (isGrounded || !doubleJump))
        {
            if (isGrounded)
            {
                doubleJump = true;
            }

            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
            jumpTimeCounter = jumpTime;
            anim.SetBool("isJumping", isJumping);
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            isJumping = false;
            anim.SetBool("isJumping", isJumping);
        }
    }

    public void onAbility(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            // Trigger the ability animation
            anim.SetTrigger("abilityTrigger");

            // Additional logic for the ability can be added here
            // For example, casting a spell, shooting a weapon, etc.
        }
    }
}

