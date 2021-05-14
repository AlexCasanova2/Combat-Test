using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckStatusPopUp : MonoBehaviour
{
    bool _ended;
    public SphereCollider spherecoll;
    void Update()
    {
        _ended = gameObject.GetComponent<AnimateDoorKeys>().ended;
       

        if (_ended)
        {
            spherecoll.enabled = false;
            gameObject.GetComponent<ShowGUIPopUpNoDestroy>().HideText();
        }
    }
}
