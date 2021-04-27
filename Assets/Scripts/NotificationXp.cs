using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationXp : MonoBehaviour
{
    Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void _EndXpAnim()
    {
        anim.SetBool("haveXp", false);
    }
}
