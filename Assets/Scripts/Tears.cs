using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tears : MonoBehaviour
{
    public GameObject tearPrefab;
    private Animator anim;
    private bool tearsSpawned; // Flag to track if tears have already been spawned

    private void Start()
    {
        anim = GetComponent<Animator>();
        tearsSpawned = false; // Initialize the flag to false
    }

    void Update()
    {
        // Check if the ability animation state is playing and tears haven't been spawned yet
        if (IsAbilityAnimationPlaying() && !tearsSpawned)
        {
            // Spawn a tear object at the same position as the onion
            GameObject tear = Instantiate(tearPrefab, transform.position, Quaternion.identity);

            // Start scaling the tear
            StartCoroutine(ScaleTear(tear));

            tearsSpawned = true; // Set the flag to true to indicate tears have been spawned
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

    IEnumerator ScaleTear(GameObject tear)
    {
        float elapsedTime = 0f;
        Vector3 initialScale = tear.transform.localScale;
        Vector3 targetScale = initialScale * 2f; // Expand tear to twice its initial size
        float duration = 2f; // Duration of expansion in seconds

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            tear.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }

        // Destroy the tear after expanding
        Destroy(tear);
    }
}
