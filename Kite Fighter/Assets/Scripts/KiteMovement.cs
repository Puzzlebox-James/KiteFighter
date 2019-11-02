using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiteMovement : MonoBehaviour
{
    // The target that the kite will rotate to look at, set in the inspector
    public Transform kiteTransfom;

    // Update is called once per frame
    void Update()
    {

        // Pull in information from the Input class
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");


        // Change transform.position based on the axes
        Vector3 pos = transform.position;
        pos.x += xAxis * 10 * Time.deltaTime;
        pos.y += yAxis * 10 * Time.deltaTime;

        transform.position = pos;

        // Quaternion rotation stuff
        Vector3 relativePos = kiteTransfom.position - transform.position;
        // The second argumnet, up, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;

        GetComponent<Rigidbody>().AddForce(relativePos.normalized);
    }
}
