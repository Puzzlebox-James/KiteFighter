using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadBob : MonoBehaviour
{
    private Vector3 sinBob;

    private void Start()
    {
        sinBob = transform.position;
    }

    void Update()
    {
        transform.position = sinBob + new Vector3(0.0f, Mathf.Sin(Time.time), 0.0f) / 10;
    }
}
