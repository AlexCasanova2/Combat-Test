using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemies : MonoBehaviour
{
    public GameObject instantiateEnemies;

    private void OnTriggerEnter(Collider other)
    {
        instantiateEnemies.SetActive(true);
    }
}
