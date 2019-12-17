using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleKiteSway : MonoBehaviour
{
    private Vector3 sinBob;
    private float bobDelay;

    private void Start()
    {
        sinBob = transform.position;
        bobDelay = Random.Range(0, 2);
    }

    void Update()
    {
        transform.position = sinBob + new Vector3(Mathf.Cos(Time.time), Mathf.Sin(Time.time + bobDelay), 0.0f) * 2;
    }
}
