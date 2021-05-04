using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompletedCheck : MonoBehaviour
{
    public Animator anim;
    public GameObject questComplete;


    public void FinishAnimation()
    {
        anim.SetBool("FinishAnimation", true);
        
        
    }
    public void HidePanel()
    {
        questComplete.SetActive(false);
    }
}
