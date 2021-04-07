using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{
    public bool canClimb;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canClimb = true;
            Debug.Log("Puedes escalar");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        canClimb = false;
    }
}
