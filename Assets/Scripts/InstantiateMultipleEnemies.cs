using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateMultipleEnemies : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<GameObject> spanwPositions = new List<GameObject>();


    void Start()
    {
        InstantiateEnemiesPrefabs();
    }

    public void InstantiateEnemiesPrefabs()
    {
        for (int i = 0; i < spanwPositions.Count; i++)
        {
            GameObject var =  Instantiate(enemyPrefab, spanwPositions[i].gameObject.transform.position, Quaternion.identity);
            var.GetComponentInChildren<Enemy>().activeState = Enemy.States.wait;
        }
    }
}
