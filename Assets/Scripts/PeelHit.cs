using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeelHit : MonoBehaviour
{
    public float damage = 10f;  // The damage the peel does
    public GameObject owner;  // The player who launched the peel

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with object: " + collision.gameObject.name);  // Log every collision for debugging

        // First, ensure the collided object is not the owner itself
        if (collision.gameObject == owner)
        {
            Debug.Log("Peel collided with owner. No action taken.");
            return;  // Exit if the peel hits the owner, preventing any further checks or actions
        }

        // Next, check if the collided object is a player other than the owner
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.TakeDamage(damage);
                Debug.Log("Applied " + damage + " damage to " + collision.gameObject.name);
            }
            else
            {
                Debug.Log("HealthManager not found on the player.");
            }
            Destroy(gameObject); // Destroy the peel after it hits
        }
        else
        {
            Debug.Log("Collision with non-player object: " + collision.gameObject.name + ", no damage applied.");
        }
    }
}
