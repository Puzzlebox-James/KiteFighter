using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

//=======================================================================================================================//
//======================================================== TO DO ========================================================//
//=======================================================================================================================//

// KITES WILL HAVE DEFINED ATTRIBUTES, SPECIFIC TO EACH KITE PREFAB. ENSURE VARIABLE EDITING IS PREFAB SPECIFIC. AND LOGICAL
// MOVE METHODS THAT ARE NOT ABSOLUTELY necessary TO KITE MOVEMENT TO THEIR OWN CLASS.



public class KiteMovement : MonoBehaviour
{

    [Header("Set in the Inspector!")]
    public float speed;

    // These varables are used to calucate kiteship not instantly stopping when there's no direction on the sticks / facing a new direction.
    private float coasttime;
    public Vector3 forwardBoostVector;
    private Vector3 driftVector; // not implemented

    // Variables to store knockback information
    [Header("Knockback Parameters")]
    public float knockbackSpeed = 50;
    public float knockbackMultiplier = 4;
    public float groundDamage = 10;

    private Vector3 knockedStartPos;
    private Vector3 knockedEndPos;
    private float knockedStartTime;
    private float knockedLength;

    // These store the input vectors from the Gamepad thumbsticks.
    [Header("These are the thumbstick vectors")]
    public Vector2 leftStickVector;
    public Vector2 rightStickVector;
    private Vector2 lastStickVector;

    // This is the main movement vector for the kiteship.
    private Vector3 velocity;
    private bool canMove = true;
    private bool knocked = false;

    // This is for the wind stuff
    public static bool windOn = true;
    public GameObject windArrowWithScript;

    // These are for endgame things
    public GameObject victory;
    public GameObject lose;
    public GameObject goBack;
    private bool isDead = false;


    private void Start()
    {
        goBack.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == false)
        {
            if (canMove == true)
            {
                Move();

                if (windOn == true)
                {
                    Wind();
                }
            }

            if (knocked == true)
            {
                KnockBack();
            }

            SphereConstrain();
        }
    }


    //=======================================================================================================================//
    //======================================================== INPUT ========================================================//
    //=======================================================================================================================//

    // These methods are assigned from the Input System - they are 'actions' that get paseed through the Player Input script on the game object. 
    public void OnLeftAnchor(InputValue lsv)
    {
        leftStickVector = lsv.Get<Vector2>();
    }
    public void OnRightAnchor(InputValue rsv)
    {
        rightStickVector = rsv.Get<Vector2>();
    }


    // This is a hack for prototype purposes
    public void OnBack()
    {
        if(isDead == true)
        SceneManager.LoadScene("Title");

    }


    //==========================================================================================================================//
    //======================================================== MOVEMENT ========================================================//
    //==========================================================================================================================//

    // https://www.seattleairgear.com/kp101.htm Here is some information about how the kite should maneuver.

    // in addition to having the plaver mave the kite, consider adding a 'pull' fuction on one of the triggers to get it to move
    // more like a real kite. Though don't worry about that too much.   

    // This could technically use a refactor to use Quaternion.AngleAxis and direction vectors to deal with rotations better.
    public void Move()
    {
        // The velocity vector is used to combine the kiteships various influence vectors into its final movement vector.
        velocity = transform.position;

        // Create a rotation vector based off stick position difference and rotate based off it.
        float stickOffset = (leftStickVector.y - rightStickVector.y) * 2;
        Vector3 rotationVector = new Vector3(0, 0, -stickOffset);
        transform.Rotate(rotationVector, Space.Self);

        // We need to apply 'upwards' velocity to the kiteship, with increasing velocity the more upside down it is.
        // We do this by getting the rotation about the z axis, squashing it into a float between 0 - 3, sigmoid style
        // Then apply that as a scalar to the local up varable of the kiteship.

        float angle = transform.eulerAngles.z;

        // Here we seperate the angle into a 3 section pie based on the z angle.
        // This is done so we can use two sigmoids to get the behavoir we want and to calculate the nessasary forward velocity to add.
        if (angle <= 10 || angle >= 350)
        {
            // Do some stuff in the future probably, for now nothing.
            forwardBoostVector *= .99f;
        }
        else if (angle > 10 && angle <= 180)
        {
            forwardBoostVector *= .99f;
            Vector3 boostotesto = new Vector3();
            boostotesto = ((Sigmoid90(angle) * transform.up) * speed * Time.deltaTime);     // Check if the angle speed we would add is greater than what we already have.
            if (forwardBoostVector.magnitude < boostotesto.magnitude)
            {
                // squishify the angle to a reasonable value to be used as a scalar
                angle = Sigmoid90(angle);
                forwardBoostVector = (angle * transform.up) * speed * Time.deltaTime;
            }
            forwardBoostVector = transform.up * forwardBoostVector.magnitude;               // If it's not, then just adjust the vector to align.
        }
        else if (angle > 180 && angle < 350)                                                // Same stuff, but facing the other way.
        {
            forwardBoostVector *= .99f;
            Vector3 boostotesto = new Vector3();
            boostotesto = ((Sigmoid270(angle) * transform.up) * speed * Time.deltaTime);
            if (forwardBoostVector.magnitude < boostotesto.magnitude)
            {
                // squishify the angle to a reasonable value to be used as a scalar
                angle = Sigmoid270(angle);
                forwardBoostVector = (angle * transform.up) * speed * Time.deltaTime;
            }
            forwardBoostVector = transform.up * forwardBoostVector.magnitude;
        }

        velocity += forwardBoostVector;

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
            coasttime = 5f; // this needs to be an inspector value. Could even be an animation curve to make things easier.
            lastStickVector = leftStickVector + rightStickVector;
        }

        //transform.position = velocity;
    }


    public void SphereConstrain()
    {
        // Here we set the depth of the kiteship based on it's x and y position.
        float xAxis = transform.position.x;
        float yAxis = transform.position.y;
        float influenceVectorZ = (Mathf.Abs(xAxis) + Mathf.Abs(yAxis));
        velocity.z = -Mathf.Pow(influenceVectorZ, 0.6f);

        // This sets the rotation about the Y axis based on the ships x pos.
        var x = transform.eulerAngles.x;
        var z = transform.eulerAngles.z;
        transform.rotation = Quaternion.Euler(x, xAxis * 2, z);

        // Clamp the kiteship from going too far away from the orign.
        if (velocity.magnitude >= 20)
        {
            velocity = Vector3.ClampMagnitude(velocity, 30f);
        }

        transform.position = velocity;
    }


    public void Wind()
    {
        Vector3 windVector = windArrowWithScript.GetComponent<Wind>().windVector;
        windVector /= 40;
        velocity += windVector;
    }



    //===========================================================================================================================//
    //======================================================== COLLISION ========================================================//
    //===========================================================================================================================//

    public void CollisionDetected(HitDetectionPasser hit)
    {
        // First check which of our hit and hurt boxes got hit, then check the enemy.
        if(hit.CompareTag("Hitbox"))
        {
            if (hit.enemyRigidbody.tag == "Hitbox")
            {
                knocked = true;

                // Find and set the knockback start time, start position, end position, and length
                knockedStartTime = Time.time;
                knockedStartPos = transform.root.position;
                knockedEndPos = (knockedStartPos + (knockedStartPos - hit.enemyPosition.position) + hit.enemyVelocity) * knockbackMultiplier;
                knockedLength = Vector3.Distance(knockedStartPos, knockedEndPos);
            }
            else if(hit.enemyRigidbody.tag == "Hurtbox")
            {
                // successfuly hit them where it hurts.
                // maybe add some lesser knockback here.
            }
            else if (hit.enemyRigidbody.tag == "Ground")
            {
                GetComponent<HealthBars>().TakeDamage(groundDamage);

                knocked = true;
                canMove = false;

                // Find and set the knockback start time, start position, end position, and length
                knockedStartTime = Time.time;
                knockedStartPos = transform.root.position;
                knockedEndPos = (knockedStartPos + Vector3.up * 20) * knockbackMultiplier;
                knockedLength = Vector3.Distance(knockedStartPos, knockedEndPos);
            }

        }
        else if(hit.CompareTag("Hurtbox"))
        {
            if(hit.enemyRigidbody.tag == "Hitbox")
            {
                // Sends a message for us to take damage with the enemey forward boost var as added dmg.
                GetComponent<HealthBars>().TakeDamage(hit.enemyVelocity.magnitude);

                knocked = true;
                canMove = false;

                // Find and set the knockback start time, start position, end position, and length
                knockedStartTime = Time.time;
                knockedStartPos = transform.root.position;
                knockedEndPos = (knockedStartPos + (knockedStartPos - hit.enemyPosition.position) + hit.enemyVelocity) * knockbackMultiplier;
                knockedLength = Vector3.Distance(knockedStartPos, knockedEndPos);
            }
            else if(hit.enemyRigidbody.tag == "Hurtbox")
            {
                // somehow we made our hurtboxes collide.
                // Don't worry about this atm.
            }
            else if (hit.enemyRigidbody.tag == "Ground ")
            {
                GetComponent<HealthBars>().TakeDamage(groundDamage);

                knocked = true;
                canMove = false;

                // Find and set the knockback start time, start position, end position, and length
                knockedStartTime = Time.time;
                knockedStartPos = transform.root.position;
                knockedEndPos = (knockedStartPos + Vector3.up * 20) * knockbackMultiplier;
                knockedLength = Vector3.Distance(knockedStartPos, knockedEndPos);
            }
        }

        Debug.Log(hit.name);
        Debug.Log("Work PLZ");
    }


    public void KnockBack()
    {
        float distCovered = (Time.time - knockedStartTime) * knockbackSpeed;
        float fractionOfJourney = (distCovered / knockedLength);
        if (fractionOfJourney >= 1)
        {
            knocked = false;
            canMove = true;
        }
        fractionOfJourney = Mathf.Sin(fractionOfJourney * Mathf.PI * 0.5f);
        velocity = Vector3.Lerp(knockedStartPos, knockedEndPos, fractionOfJourney);

        //Vector3 lookAtRotation = Quaternion.LookRotation(knockedEndPos, Vector3.forward).eulerAngles;
        //transform.rotation = Quaternion.Euler(Vector3.Scale(lookAtRotation, new Vector3(0, 0, 1)));
    }


    public void JigglyAboutFace()
    {

    }




    //=========================================================================================================================//
    //======================================================== UTILITY ========================================================//
    //=========================================================================================================================//

    // These functions are far from perfect, but they squashes the euler rotation we get between 0 and 3, aye. Sigmoidin' cetner at a values of 90 / 180.
    // See the wikipedia on logistic function / sigmoid function for getting what the variables here do.
    public static float Sigmoid90(float value)
    {
        float k = -.05f * (value - 90); 
        return 5 / (1 + Mathf.Exp(k));
    }
    public static float Sigmoid270(float value)
    {
        float k = -.05f * -(value - 270);
        return 5 / (1 + Mathf.Exp(k));
    }


    public void EndOfGame()
    {
        // Stops all children of this kite being rendered.
        Renderer[] ren;
        ren = gameObject.GetComponentsInChildren<Renderer>();
        foreach(Renderer renny in ren)
        {
            renny.enabled = false;
        }
        
        // Spawn an explosion thing
        Instantiate(lose, transform.position, Quaternion.identity);

        goBack.SetActive(true);

        // Spawn a victory thing on the winning player
        if (gameObject.name == "KiteShipP1")
        {
            Instantiate(victory, new Vector3(1, 0.8f, -18), Quaternion.identity);
        }
        if (gameObject.name == "KiteShipP2")
        {
            Instantiate(victory, new Vector3(-1, 0.8f, -18), Quaternion.identity);
        }

        isDead = true;
    }
}
