using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxes : MonoBehaviour
{
    // Variables to store knockback information
    [Header("Knockback Parameters")]
    public float knockbackSpeed = 50;
    public float knockbackDistance = 4;

    private float startTime;
    private Vector3 startMarker;
    private Vector3 endMarker;
    private float journeyLength;

    private bool knockBackEnable = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Hitbox"))
        {

            // Knockback logic:
            // Upon collision, get the vector between the two kiteships
            // Use the X and Y components of the vector to get a position, then (as a test) add the forwards boost vector mag to it
            // 
            startTime = Time.time;
            startMarker = transform.position;
            endMarker.x = (transform.root.position.x - other.gameObject.transform.root.position.x) * knockbackDistance;
            endMarker.y = (transform.root.position.y - other.gameObject.transform.root.position.y) * knockbackDistance;
            float influenceVectorZ = (Mathf.Abs(endMarker.x) + Mathf.Abs(endMarker.y));
            endMarker.z = -Mathf.Pow(influenceVectorZ, 0.6f);
            journeyLength = Vector3.Distance(startMarker, endMarker);

            Knockback();

            //call the knockback function with the movement arrestment and do the lerps to move the kites away from each other.
            
        }
        if(other.gameObject.CompareTag("Hurtbox"))
        {
           // This needs to tell the 'hurtboxes' parent kiteship to take damage in addition to the knockback and movement arrestment.s
        }

        //Debug.Log(other.name);
        Debug.Log(gameObject.name);
    }


    private void Knockback()
    {
        KiteMovement.canMove = false;
        knockBackEnable = true;

        float distCovered = (Time.time - startTime) * knockbackSpeed;
        float fractionOfJourney = (distCovered / journeyLength);
        if (fractionOfJourney >= 1)
            knockBackEnable = false;
        fractionOfJourney = Mathf.Sin(fractionOfJourney * Mathf.PI * 0.5f);
        transform.position = Vector3.Lerp(startMarker, endMarker, fractionOfJourney);


        if (fractionOfJourney >= 1)
            knockBackEnable = false;
    }

    void Update()
    {
        if (knockBackEnable == true)
        {
            Knockback();
        } else {
            KiteMovement.canMove = true;
        }
        
    }
}
