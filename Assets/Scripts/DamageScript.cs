using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float damageAmount = 1f;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has an "Enemy" tag
        if (other.CompareTag("Enemy"))
        {
            // Get the enemy's health script (you may need to replace "Health" with your actual health script)
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            // If the enemy has a health script, apply damage
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);

                // Destroy the projectile after hitting an enemy
                Destroy(gameObject);
            }
        }
    }
}

