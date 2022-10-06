using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPad : MonoBehaviour
{
    public GameObject sphere;
    private void OnTriggerEnter(Collider other)
    {
        //change the colour of the sphere
        sphere.GetComponent<Renderer>().material.color = Color.green;
    }

    private void OnTriggerStay(Collider other)
    {
        //increase the size of the sphere by 0.01f
        sphere.transform.localScale += Vector3.one * 0.01f;
    }

    private void OnTriggerExit(Collider other)
    {
        //reset the size of the sphere
        sphere.transform.localScale = Vector3.one;
        //change the colour of the sphere
        sphere.GetComponent<Renderer>().material.color = Color.white;
    }
}
