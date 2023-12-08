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
    public TowerFire towerFire;

    void Start()
    {
        //Find map parent for full map layout
        mapParent = GameObject.Find("MapParent");

        //Define navMeshSurface with mapParent(full map)
        navMeshSurface = mapParent.GetComponent<NavMeshSurface>();

        //Check if theres not one yet
        if (navMeshSurface == null)
        {
            //Tell it to add navMesh
            navMeshSurface = mapParent.AddComponent<NavMeshSurface>();
        }

        navMeshSurface.collectObjects = CollectObjects.Children;

        // Initially bake the NavMesh
        BakeNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0)) 
        {
            // Place the tower
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Instantiate the tower
                PlaceTower(hit.point);
            }
            //Bake after placing tower to update navmesh
            BakeNavMesh();
        }
    }
    

    void PlaceTower(Vector3 position)
    {
        if (towerPrefab != null)
        {
            // Instantiate the tower prefab at the specified position
            GameObject tower = Instantiate(towerPrefab, position, Quaternion.identity);

            //Add navmesh obstacle to tower to interact with navmesh on map
            tower.AddComponent<NavMeshObstacle>();

            //Get towerfire script here so it can be applied on instantiation
            TowerFire towerFire = tower.GetComponent<TowerFire>();

            //Check for towerFire
            if (towerFire != null)
            {
                // Start firing for the instantiated tower
                towerFire.StartFiring();
            } 
        }
    }

    //Method to call at several points for rest of code
    void BakeNavMesh()
    {
        navMeshSurface.BuildNavMesh();
    }
    

}
