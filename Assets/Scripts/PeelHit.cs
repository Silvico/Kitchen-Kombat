using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeelHit : MonoBehaviour
{
    public float damage = 10f; // The damage the peel does

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);  // Check if the collision is detected
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit a player");  // Confirm it's hitting a player
            HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.TakeDamage(damage);
                Debug.Log("Applied damage");  // Confirm damage is applied
            }
            else
            {
                Debug.Log("HealthManager not found on the player");  // Check if HealthManager is missing
            }
            Destroy(gameObject); // Destroy the peel after it hits
        }
    }

}
