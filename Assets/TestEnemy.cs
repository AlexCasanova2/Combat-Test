using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    public int _damage = 5;
    public int vida = 100;
    void Start()
    {
        Debug.Log("hola");
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hola");
        if (other.CompareTag("PlayerWeapon"))
        {
            TakeDamage(_damage);
        }
    }

    public void TakeDamage(int damage)
    {
        vida -= damage;

        Debug.Log("Te han quitado: " + damage + " puntos de vida.");
        Debug.Log("Te quedan: " + vida + " puntos de vida.");
    }
}
