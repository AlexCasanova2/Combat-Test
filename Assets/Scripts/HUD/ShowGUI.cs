using UnityEngine;
using UnityEngine.UI;

public class ShowGUI : MonoBehaviour
{
    SphereCollider coll;
    public GameObject text;
    private Text _text;
    private int contador;
    public string textToShow;
    void Start()
    {
        contador = 0;
        coll = GetComponent<SphereCollider>();
        _text = text.gameObject.GetComponentInChildren<Text>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && coll.CompareTag("Pickup"))
        {
            _text.text = textToShow;
            text.SetActive(true);
            //Pickup();
            //AllToDo();
        }

        if (other.CompareTag("Player") && coll.CompareTag("Pickup") && Input.GetButton("Interact"))
        {
            Pickup();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        text.SetActive(false);
        //Destroy(coll);
    }

    //Funcion para que desaparezca el mensaje en un tiempo determinado
    public void AllToDo()
    {
        text.SetActive(true);
        contador++;
        
        if (contador >= 500)
        {
            Destroy(coll);
            text.SetActive(false);
        }
    }

    //Funcion para que desaparezca el mensaje cuando se interactue
    public void Pickup()
    {
        text.SetActive(false);
        Destroy(coll);
    }

}
