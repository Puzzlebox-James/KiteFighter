using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lines : MonoBehaviour
{
    // This GameObject is for storing the Line Prefab object that has the lineRenderer component attached which has all the attributes.
    [SerializeField]
    private GameObject LinePrefab;

    private Vector3 KitePos;

    private void Update()
    {
        
    }

    private void SpawnLinePrefab()
    {
        GameObject lineGen = Instantiate(LinePrefab);
        LineRenderer lineRenderer = lineGen.GetComponent<LineRenderer>();
    }
}
