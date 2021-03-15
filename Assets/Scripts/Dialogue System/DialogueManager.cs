using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityStandardAssets.Characters.ThirdPerson;

public class DialogueManager : MonoBehaviour
{
    public GameObject nameObj, dialogueObj, continueObj;

    public TextMeshProUGUI nameText, dialogueText;
    public Animator anim;
    public Queue<string> sentences;
    public Camera cam1, cam2;
    public bool isFinished;
    public GameObject spawnEnemies;
    PlayableDirector _spawnEnemies;
    
    void Start()
    {
        sentences = new Queue<string>();
        _spawnEnemies = spawnEnemies.GetComponent<PlayableDirector>();
    }
    private void Update()
    {
        isFinished = false;
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
          Debug.Log("First");
          FindObjectOfType<CharacterStats>().isTalking = false;
          anim.SetBool("OpenClose", false);
          cam2.depth = 1;
          cam1.depth = 0;
          FindObjectOfType<ThirdPersonCharacter>().enabled = true;
          FindObjectOfType<ThirdPersonUserControl>().enabled = true;
          FindObjectOfType<DialogueTrigger>().uiTalk.SetActive(true);
          if (!isFinished) isFinished = true;
          _spawnEnemies.Play();
    }

    public void EndSecondDialogue()
    {

        Debug.Log("Second");
        FindObjectOfType<CharacterStats>().isTalking = false;
        anim.SetBool("OpenClose", false);
        cam2.depth = 1;
        cam1.depth = 0;
        FindObjectOfType<ThirdPersonCharacter>().enabled = true;
        FindObjectOfType<ThirdPersonUserControl>().enabled = true;
        FindObjectOfType<DialogueTrigger>().uiTalk.SetActive(true);
        if (!isFinished) isFinished = false;
    }
   
   
}