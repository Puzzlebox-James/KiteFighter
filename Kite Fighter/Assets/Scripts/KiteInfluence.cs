using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiteInfluence : MonoBehaviour
{
    private GameObject kiteShip;
    void Start()
    {
        kiteShip = GameObject.FindWithTag("KiteShip");
    }


    // Update is called once per frame
    void Update()
    {
        // Move the KiteShip around the 1/4th sphere depending on it's location
        SphereBound(kiteShip);
    }

    void SphereBound(GameObject kiteship)
    {
        // vectors needed for binding,
        // X line determines the z depth the ships moves inwards twoards
        // Y line does the same for z in Y
        // X line diveation creates rotation arund the Y axis
    }
}
