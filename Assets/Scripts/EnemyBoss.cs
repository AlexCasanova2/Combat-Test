using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public bool isDead;
    public GameObject canvas;
    GameObject gameFinished;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        
    }
    void Update()
    {
        isDead = gameObject.GetComponent<Enemy>().dead;
        CheckifisDead();
    }

    public void CheckifisDead()
    {
        if (isDead)
        {
            gameFinished = canvas.transform.GetChild(16).gameObject;
            gameFinished.SetActive(true);
        }
    }
}
