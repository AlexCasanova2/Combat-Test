using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateDoor : MonoBehaviour
{
    Animator anim;
    AudioSource audioSource;
    bool _isFinished;
    public GameObject engine, instantiateBoss;
    int totalEnginesCompleted;
    //public GameObject checkObjective;

    [Header("GameController")]
    public GameObject gameController;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        totalEnginesCompleted = gameController.GetComponent<GameController>().totalEnginesCompleted;

        if (_isFinished) { return; }
        
        //Debug.Log("totalEnginesCompleted " + totalEnginesCompleted);
        
        if (totalEnginesCompleted >= 3)
        {
            CheckAnim();
        }

    }

    public void CheckAnim()
    {
        anim.SetBool("isFinished", true); 
    }
    public void CheckObjective()
    {
        
        //checkObjective.SetActive(true);
    }
    public void PlaySound()
    {
        instantiateBoss.GetComponent<InstantiateEnemyBoss>().canInstantiate = true;
        audioSource.Play();
    }
}
