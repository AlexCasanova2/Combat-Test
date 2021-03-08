using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;

    //Objeto que vamos a activar cuando se use en el inventario
    public GameObject activateGameobject;


    //UI to pickup
    public Text textui;
    public GameObject pickup;

    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;

    public virtual void Interact()
    {
        Debug.Log("Interactuando con: " + transform.name);
    }

    private void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                pickup.SetActive(false);
                hasInteracted = true;
            }
        }
    }

    public void Activar(){

        activateGameobject.SetActive(true);
    
    }
    public void Desactivar()
    {

        activateGameobject.SetActive(false);

    }


    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }


    


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            pickup.SetActive(true);
            textui.text = "Press 'E' to pick up " + transform.name;
        }
        if (Input.GetKeyDown(KeyCode.E) && other.tag == "Player")
        {
            Interact();
            pickup.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            pickup.SetActive(false);
            //textui.text = "Press 'E' to pick up " + transform.name;
        }
    }

}
