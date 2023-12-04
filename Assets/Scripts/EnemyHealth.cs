using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 1000f;
    private float currentHealth;

    private void Start()
    {
        // Initialize the current health to the maximum health when the enemy is spawned
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        // Reduce the enemy's health by the damage amount
        currentHealth -= damage;

        // Check if the enemy is defeated
        if (currentHealth <= 0)
        {
            // Call a method to handle the defeated state (e.g., play an animation, spawn effects, etc.)
            Die();
        }
    }

    private void Die()
    {
        // Handle the defeated state here
        // For example, play an animation, spawn effects, or perform any other actions
        Destroy(gameObject); // Destroy the enemy GameObject
    }
}
