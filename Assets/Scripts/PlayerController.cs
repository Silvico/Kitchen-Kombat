using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float speed;
    private Rigidbody2D rb;

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

        if ((isGrounded || !doubleJump) && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            if (isGrounded)
            {
                doubleJump = true;
            }
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (isJumping && jumpTimeCounter > 0)
            {
                anim.SetBool("isJumping", isJumping);
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            isJumping = false;
            anim.SetBool("isJumping", isJumping);
        }

        float moveInput = horizontalMovement;
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        anim.SetBool("isRunning", moveInput != 0);

        if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        // Check for right mouse button click
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetTrigger("abilityTrigger");
        }
    }
    public void onMove(InputAction.CallbackContext ctx)
    {
        Vector2 movement = ctx.ReadValue<Vector2>();
        horizontalMovement = movement.x;
        Debug.Log("Movement Input: " + horizontalMovement); 
    }

   
}


