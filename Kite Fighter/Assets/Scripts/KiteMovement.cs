using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KiteMovement : MonoBehaviour
{

    public Vector2 leftStickVector;
    public Vector2 rightStickVector;

    public Vector2 velocity;

    public int speed;


    // Start is called before the first frame update
    void Awake()
    {

    }

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
        velocity = leftStickVector + rightStickVector + velocity;
        if (velocity.magnitude >= 3)
        {
            velocity = Vector2.ClampMagnitude(velocity, 3f);
        }

        transform.position = velocity;
    }
}
