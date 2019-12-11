using UnityEngine;

public class HitDetectionPasser : MonoBehaviour
{

    public Vector3 enemyVelocity;
    public Transform enemyPosition;
    public Rigidbody enemyRigidbody;

    private void OnTriggerEnter(Collider other)
    {
        // Check to see what thing we hit
        if (other.transform.root.tag == "KiteShip")
        {
            enemyVelocity = other.transform.root.GetComponent<KiteMovement>().forwardBoostVector;
            enemyPosition = other.gameObject.transform.root;
            enemyRigidbody = other.attachedRigidbody;

            // Pass this class to the KiteMovement script so I can use its datazzz
            transform.root.GetComponent<KiteMovement>().CollisionDetected(this);
        }
        else if (other.transform.root.tag == "Ground")
        {
            enemyVelocity = new Vector3(0, 0, 0);
            enemyPosition = gameObject.transform.root;
            enemyRigidbody = other.attachedRigidbody;
            transform.root.GetComponent<KiteMovement>().CollisionDetected(this);
        }
        else

            Debug.Log("KiteShip hit something without a proper tag");

    }
}
