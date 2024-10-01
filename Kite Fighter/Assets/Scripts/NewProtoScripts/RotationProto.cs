using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationProto : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate((Vector3.forward), 200 * Time.deltaTime, Space.Self);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.InverseTransformVector(Vector3.up), transform.position);
    }

}
