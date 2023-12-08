using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TowerFire : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject enemyPrefab;
    public float fireRate = 100;
    private bool canFire = false;

    public void StartFiring()
    {
        //Set bool to true for FireProjectilesMethod
        canFire = true;
        //Call fire method
        StartCoroutine(FireProjectiles());
    }

    private IEnumerator FireProjectiles()
    {
        //Do a check for bool so when false it doesn't fire, and when true it does
        while (canFire)
        {
            //Call method with firing logic
            FireProjectile();
            //Set fire rate
            yield return new WaitForSeconds((float)(.25 / fireRate));
        }
    }
    private void FireProjectile()
    {
        //Check for enemies
        if (projectilePrefab != null && enemyPrefab != null)
        {
            // Find enemy via tag
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            //Check for more than 0 enemies
            if (enemies.Length > 0)
            {
                //Call targeting logic for nearest enemy
                GameObject closestEnemy = FindClosestEnemy(enemies);

                //Check for enemy based on targeting
                if (closestEnemy != null)
                {
                    // Calculate the direction from the tower to the enemy
                    Vector3 directionToEnemy = (closestEnemy.transform.position - transform.position).normalized;

                    // set range variables
                    float firingRange = 20f;
                    float distanceToEnemy = Vector3.Distance(transform.position, closestEnemy.transform.position);

                    // Check if the enemy is within the firing range
                    if (distanceToEnemy <= firingRange)
                    {
                        // Instantiate the projectile at the tower's position
                        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

                        // Set the projectile's position to match the tower's position
                        projectile.transform.position = transform.position;

                        // Get Rigidbody of projectile
                        Rigidbody projectileRBody = projectile.GetComponent<Rigidbody>();

                        if (projectileRBody != null)
                        {
                            //Set speed of projectile
                            float projectileSpeed = 50f;
                            projectileRBody.velocity = directionToEnemy * projectileSpeed;

                            // Destroy the projectile in case of miss
                            float projectileLifetime = 1.5f;
                            Destroy(projectile, projectileLifetime);
                        }
                    }
                }
            }
        }
    }

    //Method to determine logic for calculating nearest enemy
    private GameObject FindClosestEnemy(GameObject[] enemies)
    {
        GameObject closestEnemy = null;
        float closestDistance = float.MaxValue;

        //Iterate through list of spawned enemies
        foreach (GameObject enemy in enemies)
        {
            //define distance based on positions
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            //Logic declaring closest enemy
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }
}



