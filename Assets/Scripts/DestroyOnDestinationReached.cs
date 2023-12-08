using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DestroyOnDestinationReached : MonoBehaviour
{
    private SpawnEnemies spawnEnemies;

    public SpawnEnemies SpawnEnemiesReference
    {
        set { spawnEnemies = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check for end target
        if (other.CompareTag("End"))
        {
            // Remove enemy when it makes contact with the end target
            Destroy(gameObject);

            //Notify spawn script enemy reached destination for counter
            spawnEnemies.EnemyReachedDestination();
        }
    }
}
