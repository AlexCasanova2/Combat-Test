using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{

    Collider coll;
    public ParticleSystem[] destroy;
    bool isDestroyed;
    public AudioSource audioSource;
    public int count = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        coll = GetComponent<Collider>();
        isDestroyed = false;
    }

    
    void Update()
    {
        if (isDestroyed)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "PlayerWeapon")
        {
            if (count <= 7)
            {
                foreach (ParticleSystem a in destroy)
                {
                    a.Play();
                    count++;
                    audioSource.Play(0);
                }
            }
            if (count >= 8)
            {
                isDestroyed = true;
                audioSource.Play();
            }
           
        }
    }
}
