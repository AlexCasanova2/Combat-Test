using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationReach : MonoBehaviour
{

    public bool playerEntered;

    private void Update()
    {
        //Debug.Log("playerEntered: " + playerEntered);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerEntered = true;
            //Debug.Log("playerEntered: " + playerEntered);
        }
    }
}
