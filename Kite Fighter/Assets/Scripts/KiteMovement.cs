using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KiteMovement : MonoBehaviour
{

    [Header("Set in the Inspector!")]
    public float speed;

    // This varable is used to calucate kiteship not instantly stopping when there's no direction on the sticks.
    private float coasttime;

    // These store the input vectors from the Gamepad thumbsticks.
    [Header("These are the thumbstick vectors")]
    public Vector2 leftStickVector;
    public Vector2 rightStickVector;
    private Vector2 lastStickVector;


    void Awake()
    {

    }

    // These methods are assigned from the Input System - they are 'actions' that get paseed through the Player Input script on the game object. 
    public void OnLeftAnchor(InputValue lsv)
    {
        leftStickVector = lsv.Get<Vector2>();
    }
    public void OnRightAnchor(InputValue lsv)
    {
        rightStickVector = lsv.Get<Vector2>();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
    }


    public void Move()
    {
        // The velocity vector is used to combine the kiteships various influence vectors into its final movement vector.
        Vector2 velocity = transform.position;

        // Check to see if there's no input, if there isn't then coast to 0, otherwise move.
        if ((leftStickVector.x + rightStickVector.x + leftStickVector.y + rightStickVector.y) == 0)
        {
            // coasttime heads to 0.
            coasttime = coasttime * 0.95f;
            velocity.x += lastStickVector.x * speed * Time.deltaTime * coasttime;
            velocity.y += lastStickVector.y * speed * Time.deltaTime * coasttime;
        }
        else
        {
            velocity.x += (leftStickVector.x + rightStickVector.x) * speed * Time.deltaTime;
            velocity.y += (leftStickVector.y + rightStickVector.y) * speed * Time.deltaTime;

            // Reset the coasttime and store the stick vectors
            coasttime = 5f;
            lastStickVector = leftStickVector + rightStickVector;
        }

        // Do some rotation
        float stickOffset = leftStickVector.y - rightStickVector.y;
        transform.Rotate(0, 0, -stickOffset, Space.Self);

        // Clamp the kiteship from going too far away from the orign.
        if (velocity.magnitude >= 20)
        {
            velocity = Vector2.ClampMagnitude(velocity, 20f);
        }

        transform.position = velocity;
    }
}
