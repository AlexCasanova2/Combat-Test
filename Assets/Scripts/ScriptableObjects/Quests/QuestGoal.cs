using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

[System.Serializable]
public class QuestGoal
{

    public GoalType goalType;
    public int requiredAmonut;
    public int currentAmount;

    public bool reachedLocation;

    public bool haveTalked;

    
    public bool isTalked()
    {
        return haveTalked;
    }
    public bool isReached()
    {
        return reachedLocation;
        
    }

    public void EnemyKillded()
    {
        if (goalType == GoalType.Kill)
        {
            currentAmount++;
        }
    }

    public void ItemCollected()
    {
        if (goalType == GoalType.Gathering)
        {
            currentAmount++;
        }
    }

    public void LocationReached()
    {
        if (goalType == GoalType.Explore)
        {
            reachedLocation = true;
        }
    }

    public void Talked()
    {
        if (goalType == GoalType.Talk)
        {
            haveTalked = true;
        }
    }
}

public enum GoalType
    {
        Kill,
        Explore,
        Talk,
        Gathering
    }

