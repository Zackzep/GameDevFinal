using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    
    public float rotationSpeed = 60f;
    public Transform centralPoint;
    

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateAroundCentralPoint(rotationSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            RotateAroundCentralPoint(-rotationSpeed);
        }
    }
    
    void RotateAroundCentralPoint(float rotationAmount)
    {
        // Calculate the rotation axis
        Vector3 rotationAxis = Vector3.up;

        // Rotate the camera around the central point
        transform.RotateAround(centralPoint.position, rotationAxis, rotationAmount * Time.deltaTime);
    }
}
