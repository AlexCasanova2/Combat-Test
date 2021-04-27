using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateDoorKeys : MonoBehaviour
{
    Animator anim;
    bool _haveKeys;
    public GameObject gameController;

    void Update()
    {
        _haveKeys = gameController.GetComponent<GameController>().haveKeys;
    }
    /*
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entra");
        }
        if (other.CompareTag("Player") && _haveKeys && Input.GetKey(KeyCode.E))
        {
            Debug.Log("Click");
            anim.SetBool("haveKeys", true);
        }
    }*/

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            Debug.Log("Hola");
        }

    }
}
