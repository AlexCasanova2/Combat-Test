using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityStandardAssets.Characters.ThirdPerson;

public class DialogueManager : MonoBehaviour
{
    public GameObject nameObj, dialogueObj, continueObj, talkObj;

    public TextMeshProUGUI nameText, dialogueText;
    public Animator anim;
    public Queue<string> sentences;
    public Camera cam1, cam2;
    public bool isFinished;
    public GameObject spawnEnemies;
    public GameObject cleric1;
    int vecescontadas = 0;
    public GameObject animatorCamera;

    public bool haveTalked;
    //bool cursorLockedVar;
    

    void Start()
    {
        sentences = new Queue<string>();
    }
    private void Update()
    {
        isFinished = false;
        if (haveTalked) return;
    }
    public void StartDialogue(Dialogue dialogue)
    {
        
        

        nameObj.SetActive(true);
        dialogueObj.SetActive(true);
        continueObj.SetActive(true);

        anim.SetBool("OpenClose", true);
        nameText.text = dialogue.name;

        sentences.Clear();
        foreach (string  sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
            if (sentences.Count == 0)
            {
            EndDialogue();
                return;
            }
            if (sentences.Count == sentences.Count)
            {
                cam1.depth = 1;
                cam2.depth = 0;
            }

            string sentece = sentences.Dequeue();
            dialogueText.text = sentece;
    }

    public void EndDialogue()
    {
        vecescontadas++;
        FindObjectOfType<CharacterStats>().isTalking = false;
        anim.SetBool("OpenClose", false);
        cam2.depth = 1;
        cam1.depth = 0;
        FindObjectOfType<ThirdPersonCharacter>().enabled = true;
        FindObjectOfType<ThirdPersonUserControl>().enabled = true;
        FindObjectOfType<CharacterStats>().playerCamera.enabled = true;
        FindObjectOfType<DialogueTrigger>().uiTalk.SetActive(true);
        if (!isFinished) isFinished = true;
        //Debug.Log(vecescontadas);
        animatorCamera.GetComponent<Animator>().SetBool("EndDialogue", true);
        cleric1.GetComponent<DialogueTrigger>().isTalking = false;
        SpawnEnemies();
        haveTalked = true;
        cleric1.GetComponent<BoxCollider>().enabled = false;
        talkObj.SetActive(false);

        Cursor.visible = !Cursor.visible;

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void SpawnEnemies()
    {
        if (vecescontadas <= 1)
        {
            //Debug.Log("Spawneo");
            spawnEnemies.GetComponent<InstantiateEnemies>().InstantiateEnemy();

        }
        if (vecescontadas >= 2)
        {
            Debug.Log("No hagas nada");
        }
    }
   
}