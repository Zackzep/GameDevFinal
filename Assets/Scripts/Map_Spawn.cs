using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Spawn : MonoBehaviour
{
    public GameObject[] prefabs;
    public float prefabSize = 30f;
    public float spacing = 0f;
    public int gridSize = 3;

    // Start is called before the first frame update
    void Start()
    {
        ShuffleArray(prefabs);
        Vector3 startPos = transform.position;
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                float offsetX = i * (prefabSize + spacing);
                float offsetY = j * (prefabSize + spacing);

                Vector3 spawnPosition = new Vector3(startPos.x + offsetX, startPos.y, startPos.z + offsetY);
                spawnPosition -= new Vector3(((prefabSize + spacing) * (gridSize - 1)) / 2f, 0f, ((prefabSize + spacing) * (gridSize - 1)) / 2f);

                GameObject prefabToInstantiate = prefabs[i * gridSize + j];
                Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);
            }
        }
    }

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
