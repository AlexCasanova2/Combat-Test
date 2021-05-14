using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationGold : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void _EndGoldAnim()
    {
        anim.SetBool("haveGold", false);
    }
}
