using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform endTarget;
    public float projectileSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnProjectile();
        }
    }
    void SpawnProjectile()
    {
        if (enemyPrefab != null)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.tag = "Enemy";
            NavMeshAgent enemyAgent = enemy.GetComponent<NavMeshAgent>();

            if (enemyAgent != null && endTarget != null)
            {
                enemyAgent.speed = projectileSpeed;

                enemyAgent.baseOffset = .75f;
 
                enemyAgent.avoidancePriority = 0;

                enemyAgent.SetDestination(endTarget.position);
            }
            
        }
        
    }
}
