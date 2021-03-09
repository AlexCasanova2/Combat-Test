using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject player;
    bool isBlocking;

    public void Start()
    {
        //isBlocking = player.GetComponentInChildren<PlayerController>().isBlocking;
       
    }

    private void Update()
    {
        if (isBlocking)
        {
            
        }
    }



}
