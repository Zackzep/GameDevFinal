using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButton : MonoBehaviour
{
    //Call mapsaver for access
    public MapSaver mapSaver;
    
    public void OnButtonClick()
    {
        //Call save method for button click
        mapSaver.SaveMapConfiguration();
    }
}
