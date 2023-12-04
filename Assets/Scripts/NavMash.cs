using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavMash : MonoBehaviour
{
    private GameObject mapParent;
    private NavMeshSurface navMeshSurface;
    public GameObject towerPrefab;
    public TowerFire towerFire; // Reference to the TowerFire script

    void Start()
    {
        


        mapParent = GameObject.Find("MapParent");

        if (mapParent == null)
        {
            Debug.LogError("MapParent not found. Make sure it's being created in the scene.");
            return;
        }
        navMeshSurface = mapParent.GetComponent<NavMeshSurface>();
        if (navMeshSurface == null)
        {
            navMeshSurface = mapParent.AddComponent<NavMeshSurface>();
        }

        navMeshSurface.collectObjects = CollectObjects.Children;

        // Initially bake the NavMesh
        BakeNavMesh();

        
    }

    // Update is called once per frame
    void Update()
    {
        //(KeyCode.B) placeholder for logic when placing towers
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //BuildNavMesh();
        //}

        if (Input.GetMouseButtonDown(0)) // Change to your specific condition
        {
            // Place the obstacle (you might replace this with your obstacle placement logic)
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Instantiate the obstacle at the clicked position
                PlaceTower(hit.point);
                
                
            }
            

            // Bake the NavMesh after placing the obstacle
            BakeNavMesh();
        }



    }
    

    void PlaceTower(Vector3 position)
    {
        if (towerPrefab != null)
        {
            // Instantiate the obstacle prefab at the specified position
            GameObject obstacle = Instantiate(towerPrefab, position, Quaternion.identity);

            // Optionally, you can perform additional setup or modifications to the instantiated obstacle
            // For example, you might want to add a NavMeshObstacle component to ensure it affects the NavMesh
            obstacle.AddComponent<NavMeshObstacle>();

            TowerFire towerFire = obstacle.GetComponent<TowerFire>();
            if (towerFire != null)
            {
                // Start firing for the instantiated tower
                towerFire.StartFiring();
            }
            else
            {
                Debug.LogError("TowerFire script not found on the tower prefab.");
            }
        }
        else
        {
            Debug.LogError("Obstacle prefab not assigned!");
        }
    }

    void BakeNavMesh()
    {
        // Bake the NavMesh
        navMeshSurface.BuildNavMesh();
    }
    

}
