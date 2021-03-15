using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Dialogue secondDialogue;
    public GameObject uiTalk;
    public bool isTalking;
    int count = 0;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue); 
    }

    public void TriggerSecondDialogue()
    {
        FindObjectOfType<DialogueManager>().StartSecondDialogue(secondDialogue);
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
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (count == 0)TriggerDialogue();

                if (count == 1) TriggerSecondDialogue();
                
                other.GetComponent<CharacterStats>().isTalking = true;
                other.GetComponent<CharacterStats>().Cinematica();
                isTalking = true;
                uiTalk.SetActive(false);
                count++;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) uiTalk.SetActive(false);
    }
}
