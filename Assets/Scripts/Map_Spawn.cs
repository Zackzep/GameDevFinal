using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class Map_Spawn : MonoBehaviour
{
    //Fields to determine shape and size of map, and an array to store all map pieces
    public GameObject[] prefabs;
    public float prefabSize = 30f;
    public float spacing = 0f;
    public int gridSize = 3;
    //Parent object field to work with full map
    private GameObject mapParent;

    // Start is called before the first frame update
    void Start()
    {
        //Shuffle map method call
        ShuffleArray(prefabs);
        //Tag for mapparent
        mapParent = new GameObject("MapParent");

        //Establish size and location
        Vector3 objectSize = new Vector3(prefabSize * (gridSize - 1), 0f, prefabSize * (gridSize - 1));
        Vector3 startPos = transform.position - new Vector3(objectSize.x / 2f, 0f, objectSize.z / 2f);

        //Place each piece in grid rules
        startPos += new Vector3(prefabSize / .7f, 0f, prefabSize / .7f);

        //Loop to instantiate placement based on rows and columns of grid
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                //Offsets to get spacing correct
                float offsetX = i * (prefabSize + spacing);
                float offsetY = j * (prefabSize + spacing);

                Vector3 spawnPosition = new Vector3(startPos.x + offsetX, startPos.y, startPos.z + offsetY);
                GameObject prefabToInstantiate = prefabs[i * gridSize + j];

                //Instantiate full map
                GameObject fullMap = Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);

                //Create parent
                fullMap.transform.parent = mapParent.transform;
            }
        }
        
    }

    //Method to establish layout for saving
    public MapConfiguration GetMapConfiguration()
    {
        MapConfiguration config = new MapConfiguration
        {
            prefabNames = new List<string>(),
            prefabSize = prefabSize,
            spacing = spacing,
            gridSize = gridSize
        };

        //Iterate through and save in order into list
        foreach (GameObject prefab in prefabs)
        {
            config.prefabNames.Add(prefab.name);
        }
        return config;
    }

    //Method to call on startup and shuffle array of map pieces
    void ShuffleArray(GameObject[] array)
    {
        int n = array.Length;
        for (int i = n - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            GameObject temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
