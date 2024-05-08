using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthManager healthManager = collision.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.TakeDamage(100);
                Debug.Log("Player health set to zero after hitting a corner.");
            }
            else
            {
                Debug.LogError("HealthManager component not found on the player object!");
            }
        }
    }
}
