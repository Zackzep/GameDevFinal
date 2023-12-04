using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]
public class NavMeshVis : MonoBehaviour
{
    private LineRenderer lineRenderer;

    void Start()
    {
        Debug.Log("NavMeshVisualization Start method called.");
        lineRenderer = GetComponent<LineRenderer>();
        DrawNavMesh();
    }

    void DrawNavMesh()
    {
        Debug.Log("DrawNavMesh method called.");
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        lineRenderer.positionCount = navMeshData.indices.Length;

        for (int i = 0; i < navMeshData.indices.Length; i++)
        {
            Vector3 vertex = navMeshData.vertices[navMeshData.indices[i]];
            lineRenderer.SetPosition(i, vertex);
        }
    }
}
