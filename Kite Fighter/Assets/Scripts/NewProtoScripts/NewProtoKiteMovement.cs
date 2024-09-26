using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class NewProtoKiteMovement : MonoBehaviour
{
    // The new Kite movement should use the 'flight envelope'
    //http://web.archive.org/web/20060206124000/https://www.seattleairgear.com/kp102.htm

    // Create a ball or whatever that is 'attached' to the quater surface of the sphere.
    // Make it so that it 'airspeed' (the speed at which it moves) is a funtion of the 'iso lines' no matater it's direction.

    // V apparent = [1/(L/D)][(L/D)squared + 1/2]Wcos(<PFK). L/D is constant in ideal conditions, so constant. W, wind is too.
    // So the apparent speed is just a funtion of PFK in ideal conditions.

    //Then, the groundspeed, which I think I'll be using as the defacto speed, needs to combine two axises.
    // basically, when PFK is changing or not.
    // when PFK is not changing, it's the square of (VAsqrd - Wsqrd). Which it interesting cause if PFK isn't changing Va Isn't either.
    // DON'T FORGET THAT IT DOES SCALE WITH PRK CHANGING THOUGH.

    // However, Vg-pg = (L/D)Wcos(<PFK) } Wsin(<PFK)
    // when the kite is flying along any power-gradient line when crossing <PFK's iso-power line. So when PFK is changing?
    // The term }Wsin(<PFK) is positive when the kite is flying toward the powerzone,
    // and is negative when the kite is flying away from the powerzone.



    // First steps, constrain something to that quater sphere, and get the and PFK.
    // Make it so it can move along that constrained area.


    [SerializeField] private float speed = 3;

    private Vector2 leftStickVector;
    private Vector2 rightStickVector;

    private Quaternion Rotation = Quaternion.identity;

    private Vector3 spherePosition;
    private Vector3 origin = new Vector3 (0, 0, 0);

    // These methods are assigned from the Input System - they are 'actions' that get paseed through the Player Input script on the game object.
    public void OnLeftAnchor(InputValue lsv)
    {
        leftStickVector = lsv.Get<Vector2>();
    }
    public void OnRightAnchor(InputValue rsv)
    {
        rightStickVector = rsv.Get<Vector2>();
    }


    private void Start()
    {
        spherePosition = SphericalCoordinateSystemHelpers.CartesianToSpherical(transform.position);
    }

    private void Update()
    {
        // USE A PARENT AND CHILD YOU FOOL XD

        transform.LookAt(origin);
        spherePosition.y += leftStickVector.x * speed * Time.deltaTime;
        spherePosition.z += leftStickVector.y * speed * Time.deltaTime;

        this.transform.position = SphericalCoordinateSystemHelpers.SphericalToCartesian(spherePosition);

        //transform.Rotate((transform.forward), speed * 20 * Time.deltaTime, Space.Self);
    }

}
