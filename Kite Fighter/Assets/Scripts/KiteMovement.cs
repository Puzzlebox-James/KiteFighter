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
        Vector3 velocity = transform.position;

        // Create a rotation vector based off stick position difference and rotate based off it.
        float stickOffset = (leftStickVector.y - rightStickVector.y) * 2;
        Vector3 rotationVector = new Vector3(0, 0, -stickOffset);
        transform.Rotate(rotationVector, Space.World);

        // We need to apply 'upwards' velocity to the kiteship, with increasing velocity the more upside down it is.
        // We do this by getting the rotation about the z axis, squashing it into a float between 0 - 3, sigmoid style
        // Then apply that as a scalar to the local up varable of the kiteship.

        float angle = transform.eulerAngles.z;

        // Here we seperate the behavior into 3 states, based on the z angle
        if (angle <= 10 || angle >= 350)
        {
            // Probably gonna have to add stuff here to make it feel right, but for now don't add vectoring

        }
        else if (angle > 10 && angle <= 180)
        {

            // squishify the angle to a reasonable value to be used as a scalar 
            angle = Sigmoid90(angle);
            velocity += (angle * transform.up) * speed * Time.deltaTime;

        }
        else if (angle > 180 && angle < 350)
        {

            // squishify the angle to a reasonable value to be used as a scalar 
            angle = Sigmoid270(angle);
            velocity += (angle * transform.up) * speed * Time.deltaTime;
        }

        // Now we add some direct stick input to give the kiteship some more control.
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


        // Clamp the kiteship from going too far away from the orign.
        if (velocity.magnitude >= 20)
        {
            velocity = Vector2.ClampMagnitude(velocity, 20f);
        }

        transform.position = velocity;
    }

    // These functions are far from perfect, but they squashes the euler rotation we get between 0 and 3, precious. Sigmoidin' cetner at a values of 90 / 180.
    // See the wikipedia on logistic function / sigmoid function for getting what the variables here do.
    public static float Sigmoid90(float value)
    {
        float k = -.1f * (value - 90); 
        return 5 / (1 + Mathf.Exp(k));
    }
    public static float Sigmoid270(float value)
    {
        float k = -.1f * -(value - 270);
        return 5 / (1 + Mathf.Exp(k));
    }
}
