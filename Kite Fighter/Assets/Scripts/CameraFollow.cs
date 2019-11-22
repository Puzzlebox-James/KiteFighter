﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform[] playerTransforms;

    // Grabs the transforms off all gameobjects with tag "KiteShip" into the allPlayers array
    private void Start()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("KiteShip");
        playerTransforms = new Transform[allPlayers.Length];
        for(int i = 0; i < allPlayers.Length; i++)
        {
            playerTransforms[i] = allPlayers[i].transform;
        }
    }


    public float yOffset = 2.0f;
    public float minFov = 90;
    public float maxFov = 130;
    public float cameraSpeed = .5f;
    public Transform targetTransform;


    private float xMin, xMax, yMin, yMax;

    private void LateUpdate()
    {
        if (playerTransforms.Length == 0)
        {
            Debug.Log("Have not found a player Kite, make sure the 'KiteShip' tag is on");
            return;
        }

        xMin = xMax = playerTransforms[0].position.x;
        yMin = yMax = playerTransforms[0].position.y;
        for (int i = 1; i < playerTransforms.Length; i++)
        {
            if (playerTransforms[i].position.x < xMin)
                xMin = playerTransforms[i].position.x;

            if (playerTransforms[i].position.x > xMax)
                xMax = playerTransforms[i].position.x;

            if (playerTransforms[i].position.y < yMin)
                yMin = playerTransforms[i].position.y;

            if (playerTransforms[i].position.y > yMax)
                yMax = playerTransforms[i].position.y;
        }

        float xMiddle = (xMin + xMax) / 2;
        float yMiddle = (yMin + yMax) / 2;
        float distance = xMax*2.5f - xMin*2.5f;

        if (distance < minFov)
            distance = minFov;

        if (distance > maxFov)
            distance = maxFov;

        Camera.main.fieldOfView = distance;

        targetTransform.transform.position = new Vector3(xMiddle, yMiddle, 0);
        Quaternion lookOnLook = Quaternion.LookRotation(targetTransform.transform.position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime * cameraSpeed);
    }
}
