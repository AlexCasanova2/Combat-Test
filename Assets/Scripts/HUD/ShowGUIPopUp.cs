using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowGUIPopUp : MonoBehaviour
{
    SphereCollider coll;
    public GameObject text;
    private TextMeshProUGUI _text;
    private int contador;
    public string textToShow;
    public int timeShowing;
    void Start()
    {
        contador = 0;
        coll = GetComponent<SphereCollider>();
        _text = text.gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && coll.CompareTag("Interact"))
        {
            _text.SetText(textToShow);
            AllToDo();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        text.SetActive(false);
        coll.enabled = false;
    }

    public void AllToDo()
    {
        text.SetActive(true);
        contador++;

        if (contador >= timeShowing)
        {
            coll.enabled = false;
            text.SetActive(false);
        }
    }
}
