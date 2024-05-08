using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasHit : MonoBehaviour
{
    public GameObject owner;  // The player who released the gas
    public float damage = 20f;  // Fixed damage per second
    private Dictionary<GameObject, float> playersInGas = new Dictionary<GameObject, float>();

    void OnTriggerStay2D(Collider2D collision)
    {
        // Debug log for every collision detection
        Debug.Log("Gas collision detected with object: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player") && collision.gameObject != owner)
        {
            Debug.Log("Gas is in contact with a valid player (not the owner).");
            if (!playersInGas.ContainsKey(collision.gameObject))
            {
                playersInGas[collision.gameObject] = 0; // Initialize time counter for this player
                Debug.Log("New player entered gas: " + collision.gameObject.name);
            }

            playersInGas[collision.gameObject] += Time.deltaTime; // Increment the time counter
            float timeInGas = Mathf.Floor(playersInGas[collision.gameObject]);

            if (timeInGas >= 1) // Only damage at full seconds
            {
                HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();
                if (healthManager != null)
                {
                    healthManager.TakeDamage(damage);  // Apply fixed damage
                    Debug.Log("Applied " + damage + " fixed damage to " + collision.gameObject.name);
                }
                else
                {
                    Debug.Log("HealthManager component not found on " + collision.gameObject.name);
                }

                playersInGas[collision.gameObject] = 0; // Reset the timer after applying damage
                Debug.Log("Damage applied and timer reset for " + collision.gameObject.name);
            }
        }
        else
        {
            Debug.Log("Collision with non-target or owner detected. No action taken.");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playersInGas.ContainsKey(collision.gameObject))
            {
                playersInGas.Remove(collision.gameObject); // Remove player from the dictionary when they exit the gas
                Debug.Log("Player exited gas and was removed from tracking: " + collision.gameObject.name);
            }
        }
    }
}
