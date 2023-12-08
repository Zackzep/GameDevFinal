using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapSaver : MonoBehaviour
{
    //Map_Spawn reference for necessary info
    public Map_Spawn mapSpawn;

    public void SaveMapConfiguration()
    {
        // Get the map configuration from the MapSpawn script
        MapConfiguration mapConfig = mapSpawn.GetMapConfiguration();

        // Create a JSON of the map configuration
        string jsonConfig = JsonUtility.ToJson(mapConfig);

        //Set file path
        string fileName = "mapConfig.json";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // Save the JSON to the specified file
        File.WriteAllText(filePath, jsonConfig);

        //Debug to copy path and pull up file
        Debug.Log("Map configuration saved to: " + filePath);
    }
}
