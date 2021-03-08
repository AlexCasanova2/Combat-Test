using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateObject : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.tag == "Player")
        {
            anim.SetBool("OpenChest", true);
        }
    }
}
