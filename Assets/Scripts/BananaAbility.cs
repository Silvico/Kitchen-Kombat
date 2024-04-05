using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaAbility : MonoBehaviour
{
    public GameObject peelPrefab; // Reference to the peel game object prefab
    public Transform throwPoint; // position from where peel will be thrown
    public float throwForce = 10f;
    public float rotationSpeed;

    private Animator anim;
    private bool hasThrownPeel = false;

    void Start()
    {
        anim = GetComponent<Animator>(); // Get Animator component
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the ability animation is playing and peel hasn't been thrown yet
        if (IsAbilityAnimationPlaying() && !hasThrownPeel)
        {
            StartCoroutine(ThrowPeelAfterAnimation());
            hasThrownPeel = true;
        }
    }

    bool IsAbilityAnimationPlaying()
    {
        // Check if the ability animation state is playing
        return anim.GetCurrentAnimatorStateInfo(0).IsName("bability");
    }

    IEnumerator ThrowPeelAfterAnimation()
    {
        // Wait for the animation to complete
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        // Throw the peel
        ThrowPeel();
    }

    void ThrowPeel()
    {
        // Instantiate the peel prefab at the throw point position
        GameObject peel = Instantiate(peelPrefab, throwPoint.position, Quaternion.identity);

        // Get the rigidbody component of the peel prefab
        Rigidbody2D peelRb = peel.GetComponent<Rigidbody2D>();

        // Calculate the direction to throw the peel (to the right?)
        Vector2 throwDirection = Vector2.right;

        peelRb.angularVelocity = rotationSpeed;
        // Apply force to the peel in the calculated direction
        peelRb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);

        // Set an angular velocity to make the peel rotate
    }
}
