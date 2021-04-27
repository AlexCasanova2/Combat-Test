using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuests : MonoBehaviour
{
    //public Quest quest;

    public List<Quest> quests = new List<Quest>();
    
    public void DoQuest()
    {
        foreach (var quest in quests) {

            if (quest.isActive)
            {
                quest.goal.LocationReached();
                if (quest.goal.reachedLocation)
                {
                    quest.Complete();
                }
            }
        }

        /*if (quests[0].isActive)
        {
            quests[0].goal.LocationReached();
            if (quests[0].goal.reachedLocation)
            {
                quests[0].Complete();
            }
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishQuest"))
        {
            DoQuest();
            Debug.Log("Entra");
        }
    }
}
