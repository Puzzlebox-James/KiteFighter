using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Controls))]
public class RotationProto : NewProtoKiteMovement
{
    private void FixedUpdate()
    {
        Debug.Log(_controls.LeftStickValue.y);
        this.transform.Rotate((Vector3.forward), 200 * Time.deltaTime , Space.Self);
    }
}
