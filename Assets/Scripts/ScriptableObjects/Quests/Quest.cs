﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    public bool isActive;

    public string title;
    public string description;
    public int experienceReward;
    public int goldReward;

    public QuestGoal goal;

    //Completar la quest
    public void Complete()
    {
        isActive = false;
        //Debug.Log(title + " was completed.");

    }
}
