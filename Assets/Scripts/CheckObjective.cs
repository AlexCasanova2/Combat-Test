using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObjective : MonoBehaviour
{

    //UI Objectives
    public GameObject helmetImage;
    public GameObject shieldImage;
    public GameObject swordImage;

    
    void Update()
    {
        CheckObjectives();
    }
    

    public void CheckObjectives()
    {
        if (transform.CompareTag("Helmet"))
        {
            helmetImage.SetActive(true);
        }
        if (transform.CompareTag("Shield"))
        {
            shieldImage.SetActive(true);
        }
        if (transform.CompareTag("Sword"))
        {
            swordImage.SetActive(true);
        }
    }
}
