using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    // hmm, it would seem PlayerInput needs to be on the target object to recive signals...
    // Maybe look into the singleton pattern to place on my PlayerOneInput Singleton thing to route my controlls thou? Thoughts?

    [SerializeField] private PlayerInput playerOneInput;

    public Vector2 LeftStickValue{ get; private set; }
    public Vector2 RightStickValue { get; private set; }

    // These methods are assigned from the Input System - they are 'actions' that get paseed through the Player Input script on the game object.
    private void OnLeftAncor(InputValue lsv)
    {
        LeftStickValue = lsv.Get<Vector2>();
        Debug.Log(LeftStickValue);
    }
    private void OnRightAncor(InputValue rsv)
    {
        RightStickValue = rsv.Get<Vector2>();
    }
}
