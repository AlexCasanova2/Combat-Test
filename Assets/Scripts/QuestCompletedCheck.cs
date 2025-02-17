﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompletedCheck : MonoBehaviour
{
    public Animator anim;
    public GameObject questComplete;

    public GameObject uiObjectives;

    public void FinishAnimation()
    {
        anim.SetBool("FinishAnimation", true);
    }
    public void HidePanel()
    {
        questComplete.SetActive(false);
    }

    public void Completed()
    {
        anim.SetBool("QuestCompleted", false);

        uiObjectives.SetActive(false);
    }
}
