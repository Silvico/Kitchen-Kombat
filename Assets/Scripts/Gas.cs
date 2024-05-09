using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    public GameObject gasPrefab;
    [SerializeField] private AudioClip gassound; // Reference to the gas sound effect
    private Animator anim;
    private GameObject gasInstance; // Reference to the instantiated gas GameObject
    private bool gasSpawned;
    private bool cooldown;
    private AudioSource audioSource; // AudioSource component

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // Assign the AudioSource
        gasSpawned = false;
        cooldown = false;
    }

    void Update()
    {
        if (IsAbilityAnimationPlaying() && !gasSpawned && !cooldown)
        {
            gasInstance = Instantiate(gasPrefab, transform.position, Quaternion.identity);
            gasInstance.transform.parent = transform; // Parent the gas instance to the onion

            // Set the owner of the gas to this GameObject (the onion)
            GasHit gasHitScript = gasInstance.GetComponent<GasHit>();
            if (gasHitScript != null)
            {
                gasHitScript.owner = this.gameObject; // Pass the onion as the owner
            }

            StartCoroutine(ExpandGas());
            StartCoroutine(GasCooldown());

            // Play the gas sound effect
            if (audioSource && gassound)
            {
                audioSource.PlayOneShot(gassound);
            }
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
        // Define the target scale by multiplying the initial scale by 1.5
        Vector3 targetScale = initialScale * 1.5f;

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
