using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 5);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Check if we hit the object tagged target
        if(collision.gameObject.CompareTag("Target"))
        {
            //change the colour of the target
            collision.gameObject.GetComponent<Renderer>().material.color = Color.red;
            //destroy the target after 1 second
            Destroy(collision.gameObject, 1);
            //destroy this object
            Destroy(gameObject);
        }

    }
}
