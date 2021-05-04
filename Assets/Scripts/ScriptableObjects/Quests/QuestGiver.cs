using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;

    public GameObject player;

    public GameObject questWindow;
    public Text titleText;
    public Text descriptionText;
    public Text experienceText;
    public Text goldText;

    [Header("Popup")]
    public GameObject newQuest;
    public TextMeshProUGUI questText;
    
    Animator anim;

    private void Start()
    {
        anim = newQuest.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OpenQuestWindow();
        }
    }
    public void OpenQuestWindow(){
        questWindow.SetActive(true);
        //Igualamos los valores del ScriptableObject a los textos del UI
        titleText.text = quest.title;
        //descriptionText.text = quest.description;
        //experienceText.text = quest.experienceReward.ToString();
        //goldText.text = quest.goldReward.ToString();
        AcceptQuest();
    }

    public void AcceptQuest()
    {
        quest.isActive = true;
        //Le damos la quest al player
        player.GetComponentInChildren<PlayerQuests>().quests.Add(quest);
        gameObject.SetActive(false);

        newQuest.SetActive(true);
        questText.SetText(quest.description);
        anim.SetBool("NewQuest", true);
    }
}
