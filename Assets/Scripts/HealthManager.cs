using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float maxHealth = 100f;
    private float currentHealth;
    public string playerID; // Unique identifier for the player

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            OnPlayerDefeated();
        }
    }

    void OnPlayerDefeated()
    {
        // Mark this player as defeated
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PlayerDefeated(playerID);
        }
        else
        {
            Debug.LogError("GameManager instance is not found!");
        }
    }
}


