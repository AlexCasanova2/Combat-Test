using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    public GameObject gameController;
    public bool haveKeys;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E)) { 
            haveKeys = true;
            gameController.GetComponent<GameController>().haveKeys = true; 
            Destroy(gameObject);
        } 
       
    }

}
