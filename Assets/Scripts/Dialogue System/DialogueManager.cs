using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject nameObj, dialogueObj, continueObj;

    public TextMeshProUGUI nameText, dialogueText;
    public Animator anim;
    public Queue<string> sentences;
    public Camera cam1, cam2;

    void Start()
    {
        sentences = new Queue<string>();
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

    void EndDialogue()
    {
        FindObjectOfType<CharacterStats>().isTalking = false;
        anim.SetBool("OpenClose", false);
        Debug.Log("Ended");
        cam2.depth = 1;
        cam1.depth = 0;
    }
}