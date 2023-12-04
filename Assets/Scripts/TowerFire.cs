using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TowerFire : MonoBehaviour
{
    //public GameObject towerPrefab;
    public GameObject projectilePrefab;
    public GameObject enemyPrefab;
    public float fireRate = 100;

    private bool canFire = false;

    public void StartFiring()
    {
        Debug.Log("StartFiring called!");
        canFire = true;
        //towerPosition = transform.position;
        StartCoroutine(FireProjectiles());
        
    }

    private IEnumerator FireProjectiles()
    {
        Debug.Log("FireProjectiles() coroutine started");
        while (canFire)
        {
            FireProjectile();
            yield return new WaitForSeconds((float)(.25 / fireRate));
        }
    }
    private void FireProjectile()
    {
        if (projectilePrefab != null && enemyPrefab != null)
        {
            // Find enemy via tag
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if (enemies.Length > 0)
            {
                GameObject closestEnemy = FindClosestEnemy(enemies);

                if (closestEnemy != null)
                {
                    // Calculate the direction from the tower to the enemy
                    Vector3 directionToEnemy = (closestEnemy.transform.position - transform.position).normalized;

                    // Check if the enemy is within the firing range
                    float firingRange = 20f; // Adjust the firing range as needed
                    float distanceToEnemy = Vector3.Distance(transform.position, closestEnemy.transform.position);

                    if (distanceToEnemy <= firingRange)
                    {
                        // Instantiate the projectile at the tower's position
                        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

                        // Set the projectile's rotation based on the direction to the enemy
                        projectile.transform.rotation = Quaternion.LookRotation(directionToEnemy);

                        // Set the projectile's position to match the tower's position
                        projectile.transform.position = transform.position;

                        // Get the Rigidbody component of the projectile
                        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

                        // Apply force to the projectile using NavMesh information
                        if (projectileRb != null)
                        {
                            // Use the calculated direction to apply velocity
                            float projectileSpeed = 50f; // Adjust the speed as needed
                            projectileRb.velocity = directionToEnemy * projectileSpeed;

                            // Destroy the projectile after a set amount of time (e.g., 3 seconds)
                            float projectileLifetime = 1.5f; // Adjust the lifetime as needed
                            Destroy(projectile, projectileLifetime);
                        }
                    }
                }
            }
            else
            {
                Debug.LogWarning("No enemies found in the scene.");
            }
        }
    }

    private GameObject FindClosestEnemy(GameObject[] enemies)
    {
        GameObject closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    // Optionally, you might want to stop firing when the tower is destroyed
    // private void OnDestroy()
    // {
    //     canFire = false;
    // }
}



