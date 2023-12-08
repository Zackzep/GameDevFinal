using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform endTarget;
    public float enemySpeed = 5f;
    public float initialDelay = 15f;
    public float spawnInterval = 5f;
    public float[] increaseIntervals = { 60f, 120f, 180f, 240f, 300f, 360f };
    public float[] increasedSpawnIntervals = { 4.5f, 4f, 3f, 2.5f, 2f, 1f };
    private float elapsedTime = 0f;
    //Counter work in progress
    public int enemyReachEndCounter = 100;
    

    // Start is called before the first frame update
    void Start()
    {
        
        //Call enemy spawn initially
        InvokeRepeating("SpawnEnemy", initialDelay, spawnInterval);
        //Coroutine to repeat and change over time
        StartCoroutine(IncreaseSpawnInterval());
    }

    void SpawnEnemy()
    {
        //Check for prefab
        if (enemyPrefab != null)
        {
            //Spawn enemy and attach appropriate tag and navmesh agent
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.tag = "Enemy";
            NavMeshAgent enemyAgent = enemy.GetComponent<NavMeshAgent>();

            //Check for enemy ant target
            if (enemyAgent != null && endTarget != null)
            {
                //Set enemy movement parameters
                enemyAgent.speed = enemySpeed;
                enemyAgent.baseOffset = .75f;
                enemyAgent.SetDestination(endTarget.position);

                //Remove enemy when it reaches end
                DestroyOnDestinationReached destroyScript = enemy.AddComponent<DestroyOnDestinationReached>();

                // Set the SpawnEnemies reference in DestroyOnDestinationReached
                destroyScript.SpawnEnemiesReference = this;
                
                
            }
        }
    }
    

    IEnumerator IncreaseSpawnInterval()
    {
        //Iterate through increase intervals array
        for (int i = 0; i < increaseIntervals.Length; i++)
        {
            float interval = increaseIntervals[i];

            //Set elapsed time to compare to game time and check
            while (elapsedTime < interval)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            //Keep increasing interval for length of array
            if (i < increasedSpawnIntervals.Length)
            {
                //Cancel initial rate or won't applay new spawn rate
                CancelInvoke("SpawnEnemy");
                //Set interval to current one in array based on loop
                spawnInterval = increasedSpawnIntervals[i];
                //Start again at new interval
                InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
            }
        }
    }
    public void EnemyReachedDestination()
    {

        // Decrease the counter
        enemyReachEndCounter--;

        //Display in debug
        Debug.Log("Enemy Countdown: " + enemyReachEndCounter);

    }
}
