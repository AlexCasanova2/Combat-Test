using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckStatusPopUpLargeDoor : MonoBehaviour
{
    bool _ended;
    public SphereCollider spherecoll;
    void Update()
    {
        _ended = gameObject.GetComponent<AnimateDoor>().ended;


        if (_ended)
        {
            spherecoll.enabled = false;
            gameObject.GetComponent<ShowGUIPopUpNoDestroy>().HideText();
        }
    }
}
