using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxes : MonoBehaviour
{
 
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Hitbox"))
        {
            Debug.Log("Hitbox Hit");
        }
        if(other.gameObject.CompareTag("Hurtbox"))
        {
            Debug.Log("Hurtbox Hit");
        }

        //Debug.Log(other.name);
        Debug.Log(gameObject.name);
    }
}
