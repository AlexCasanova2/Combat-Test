using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateDoorKeys : MonoBehaviour
{
    public Animator animator;
    bool _haveKeys;
    public GameObject gameController;

    void Update()
    {
        _haveKeys = gameController.GetComponent<GameController>().haveKeys;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E) && _haveKeys)
        {
            Debug.Log("Hola");
            animator.SetBool("canEnter", true);
        }

    }
}
