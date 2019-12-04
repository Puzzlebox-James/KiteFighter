using UnityEngine;

public class HitDetectionPasser : MonoBehaviour
{
    private void OnTriggerEnter()
    {
        transform.parent.root.GetComponent<KiteMovement>().CollisionDetected(this);
    }
}
