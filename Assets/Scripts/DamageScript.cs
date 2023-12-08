using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public float damageAmount = 1f;

    private void OnTriggerEnter(Collider other)
    {
        // Check if object has "Enemy" tag
        if (other.CompareTag("Enemy"))
        {
            //Get health script
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            //Check if enemy has health
            if (enemyHealth != null)
            {
                //Apply damage with built in method
                enemyHealth.TakeDamage(damageAmount);

                // Destroy the projectile on contact
                Destroy(gameObject);
            }
        }
    }
}

