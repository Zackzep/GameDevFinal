using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class Map_Spawn : MonoBehaviour
{
    public GameObject[] prefabs;
    public float prefabSize = 30f;
    public float spacing = 0f;
    public int gridSize = 3;

    private GameObject mapParent;

    // Start is called before the first frame update
    void Start()
    {
        ShuffleArray(prefabs);
        mapParent = new GameObject("MapParent");
        Vector3 objectSize = new Vector3(prefabSize * (gridSize - 1), 0f, prefabSize * (gridSize - 1));
        Vector3 startPos = transform.position - new Vector3(objectSize.x / 2f, 0f, objectSize.z / 2f);

        
        startPos += new Vector3(prefabSize / .7f, 0f, prefabSize / .7f);

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                float offsetX = i * (prefabSize + spacing);
                float offsetY = j * (prefabSize + spacing);

                Vector3 spawnPosition = new Vector3(startPos.x + offsetX, startPos.y, startPos.z + offsetY);

                GameObject prefabToInstantiate = prefabs[i * gridSize + j];
                GameObject fullMap = Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);

                fullMap.transform.parent = mapParent.transform;
            }
        }
        
    }
    //How do I use the cylinder as start position to also spawn with the map and use as spawn point?

    //Transform or colliders (get bounding box)
    //Renderer
    //DOnt mess with the scale 

    // Update is called once per frame
    void Update()
    {
        
    }

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
