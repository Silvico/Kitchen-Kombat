using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasHit : MonoBehaviour
{
    public float initialDamage = 10f; // Initial damage per second
    private float damageIncrease = 10f; // Additional damage per second for each second in the gas
    private Dictionary<GameObject, float> playersInGas = new Dictionary<GameObject, float>();

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!playersInGas.ContainsKey(collision.gameObject))
            {
                playersInGas[collision.gameObject] = 0; // Initialize time counter for this player
            }

            playersInGas[collision.gameObject] += Time.deltaTime; // Increment the time counter
            float timeInGas = Mathf.Floor(playersInGas[collision.gameObject]);

            if (timeInGas >= 1) // Only damage at full seconds
            {
                HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();
                if (healthManager != null)
                {
                    float damageToApply = initialDamage + (damageIncrease * (timeInGas - 1));
                    healthManager.TakeDamage(damageToApply);
                    Debug.Log("Applied " + damageToApply + " damage to " + collision.gameObject.name);
                }

                playersInGas[collision.gameObject] = 0; // Reset the timer after applying damage
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playersInGas.ContainsKey(collision.gameObject))
            {
                playersInGas.Remove(collision.gameObject); // Remove player from the dictionary when they exit the gas
            }
        }
    }
}
