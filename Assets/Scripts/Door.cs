using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject door1;
    public GameObject door2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //open the doors
            door1.SetActive(false);
            door2.SetActive(false);
            //instantiate some targets to fire at
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //close the doors
            door1.SetActive(true);
            door2.SetActive(true);
        }
    }
}
