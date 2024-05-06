using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaAbility : MonoBehaviour
{
    public GameObject peelPrefab;
    public Transform throwPoint;
    public float throwForce = 10f;
    public float rotationSpeed = 200f;

    private Animator anim;
    private bool hasThrownPeel = false;
    private Rigidbody2D rb;
    private Vector2 lastMovementDirection = Vector2.right;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); // Assume a Rigidbody2D is attached for movement tracking
    }

    void Update()
    {
        if (rb.velocity.x != 0) // Checks if there is horizontal movement
        {
            // Update last movement direction based on the velocity sign
            lastMovementDirection = new Vector2(Mathf.Sign(rb.velocity.x), 0);
        }

        // Check if the ability animation is playing and the peel hasn't been thrown yet
        if (IsAbilityAnimationPlaying() && !hasThrownPeel && !anim.GetBool("isNaked"))
        {
            StartCoroutine(ThrowPeelAfterAnimation());
            hasThrownPeel = true;
        }
    }

    bool IsAbilityAnimationPlaying()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("bability");
    }

    IEnumerator ThrowPeelAfterAnimation()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        GameObject peel = Instantiate(peelPrefab, throwPoint.position, Quaternion.identity);
        ThrowPeel(peel);
        anim.SetBool("isNaked", true);
        StartCoroutine(DestroyPeelAndReset(peel));
    }

    void ThrowPeel(GameObject peel)
    {
        Rigidbody2D peelRb = peel.GetComponent<Rigidbody2D>();

        // Use the last recorded movement direction to determine throw direction
        Vector2 throwDirection = lastMovementDirection;


        //RaycastHit2D hit = Physics2D.Raycast(throwPoint.position, throwDirection, 5.0f);
        //if (hit.collider != null)
        //{
        //    Debug.Log("Obstacle detected: " + hit.collider.name);
        //}

        peelRb.angularVelocity = rotationSpeed;
        peelRb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
    }

    IEnumerator DestroyPeelAndReset(GameObject peel)
    {
        yield return new WaitForSeconds(5f);
        Destroy(peel);
        anim.SetBool("isNaked", false);
        if (!IsAbilityAnimationPlaying()) // Ensure the ability animation isn't playing
        {
            hasThrownPeel = false;
        }
    }
}
