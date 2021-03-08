using UnityEngine;
using UnityEngine.UI;

public class CrouchGUI : ShowGUI
{
    SphereCollider coll;
    public GameObject text;
    private Text _text;
    private int contador;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
           
            _text.text = "Press 'C' to crouch";
            text.SetActive(true);
            contador++;
            Debug.Log(contador);
            if (contador >= 500)
            {
                Destroy(coll);

                text.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        text.SetActive(false);
        Destroy(coll);
    }
    
}
