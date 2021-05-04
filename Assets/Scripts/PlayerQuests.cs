using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerQuests : MonoBehaviour
{
    //public Quest quest;
    [Header("UI")]
    public GameObject questComplete;
    public TextMeshProUGUI questText;
    Animator anim;
    string name;

    [Header("Quests")]
    public List<Quest> quests = new List<Quest>();

    private void Start()
    {
        anim = questComplete.GetComponent<Animator>();
    }

    public void DoQuest()
    {
        foreach (var quest in quests) {

            if (quest.isActive)
            {
                quest.goal.LocationReached();
                if (quest.goal.reachedLocation)
                {
                    quest.Complete();
                   name =  quest.title;
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

            //Hacer Animacion Canvas para enseñar que la quest esta completada

            questComplete.SetActive(true);
            anim.SetBool("QuestCompleted", true);

            questText.SetText("You have completed the " + name +  " Quest. You have gained gold and experience.");


        }
    }
}
