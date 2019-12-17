using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public Vector3 windVector;

    void Awake()
    {
        windVector = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), Random.Range(-3, 3));
        Vector3 pointDirec = transform.position - windVector;
        gameObject.transform.LookAt(pointDirec);
    }
}
