using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject uiTalk;
    public bool isTalking;
    public bool ended;
    
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiTalk.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E) && !isTalking)
            {
                TriggerDialogue();
                other.GetComponent<CharacterStats>().isTalking = true;
                other.GetComponent<CharacterStats>().Cinematica();
                isTalking = true;
                uiTalk.SetActive(false);

                GameController.PauseYMouseControl(false, true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) uiTalk.SetActive(false);
    }
}
