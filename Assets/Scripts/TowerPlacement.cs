using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TowerPlacement : MonoBehaviour
{
    public GameObject towerPrefab;
    //Edits
    public TowerFire towerFire;

    //public float minDistance = 100f;
    private List<GameObject> placedTowers = new List<GameObject>();

    void Update()
    {
        // Check for user input (e.g., mouse click)
        if (Input.GetMouseButtonDown(0))  // Change to the desired input method
        {
            // Raycast to get the position on the ground where the user clicked
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the position is valid (not too close to existing obstacles)
                
                GameObject tower = InstantiateTower(hit.point);
                placedTowers.Add(tower);
                //Edits
                towerFire = GetComponent<TowerFire>();
                if (towerFire != null)
                {
                    towerFire.StartFiring();
                }
            }
        }
    }

    GameObject InstantiateTower(Vector3 position)
    {
        if (towerPrefab != null)
        {
            // Instantiate the obstacle prefab at the specified position
            GameObject tower = Instantiate(towerPrefab, position, Quaternion.identity);

            // Optionally, you can perform additional setup or modifications to the instantiated obstacle
            // For example, you might want to add a NavMeshObstacle component to ensure it affects the NavMesh
            //obstacle.AddComponent<NavMeshObstacle>();
            return tower;
        }
        else
        {
            Debug.LogError("Obstacle prefab not assigned!");
            return null;
        }
    }
    
}
