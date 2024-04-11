using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    public GameObject gasPrefab;
    private Animator anim;
    private GameObject gasInstance; // Reference to the instantiated gas GameObject
    private bool gasSpawned;
    private bool cooldown;

    private void Start()
    {
        anim = GetComponent<Animator>();
        gasSpawned = false;
        cooldown = false;
    }

    void Update()
    {
        // Check if the ability animation state is playing and gas hasn't been spawned yet and not in cooldown
        if (IsAbilityAnimationPlaying() && !gasSpawned && !cooldown)
        {
            // Spawn a gas object at the same position as the onion
            gasInstance = Instantiate(gasPrefab, transform.position, Quaternion.identity);
            // Parent the gas instance to the onion
            gasInstance.transform.parent = transform;
            // Start expanding coroutine
            StartCoroutine(ExpandGas());
            // Start cooldown
            StartCoroutine(GasCooldown());
        }
    }

    // Check if the ability animation state is playing
    bool IsAbilityAnimationPlaying()
    {
        if (anim != null)
        {
            return anim.GetCurrentAnimatorStateInfo(0).IsName("Oability");
        }
        return false;
    }

    IEnumerator ExpandGas()
    {
        gasSpawned = true; // Set the flag to true to indicate gas has been spawned

        // Get the initial scale of the gas object
        Vector3 initialScale = gasInstance.transform.localScale;
        // Define the target scale by multiplying the initial scale by 2
        Vector3 targetScale = initialScale * 2f;

        float elapsedTime = 0f;
        float expandDuration = 2f; // Duration for the gas to expand

        // Gradually increase the scale of the gas object over time
        while (elapsedTime < expandDuration)
        {
            // Calculate the current scale based on the elapsed time
            float t = elapsedTime / expandDuration;
            gasInstance.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set isOutofGas to true in the Animator controller
        anim.SetBool("isOutofGas", true);

        // Wait for 1 second (total 3 seconds)
        yield return new WaitForSeconds(1f);

        // Destroy the gas after expansion
        Destroy(gasInstance);
        gasSpawned = false;
    }

    IEnumerator GasCooldown()
    {
        // Set cooldown to true
        cooldown = true;
        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);
        // Reset cooldown after cooldown period
        cooldown = false;
        // Set isOutofGas back to false in the Animator controller
        anim.SetBool("isOutofGas", false);
    }
}

