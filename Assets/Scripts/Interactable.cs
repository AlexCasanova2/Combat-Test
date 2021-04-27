using TMPro;
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

    [Header("UI")]
    //UI Objectives
    public GameObject helmetImage;
    public GameObject shieldImage;
    public GameObject swordImage;

    [Header("UI Tutorial")]
    public GameObject tutorialUI;
    //public Text tutorialText;
    public TextMeshProUGUI tutorialText;
    public string textToShow;
    [SerializeField]
    bool isActivated;
    public GameObject gameController;

    public virtual void Interact()
    {
        //Debug.Log("Interactuando con: " + transform.name + " " + transform.tag);

        ShowTutorial();

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

    public void ShowTutorial()
    {
        tutorialUI.SetActive(true);
        tutorialText.SetText(textToShow);
        gameController.GetComponent<GameController>().tutorialUI = true;
        isActivated = true;
    }


    public void Activar()
    {
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
        if (other.CompareTag("Player"))
        {
            pickup.SetActive(true);
            textui.text = "Press 'E' to pick up " + transform.name;
        }
        if (Input.GetKeyDown(KeyCode.E) && other.CompareTag("Player"))
        {
            Interact();
            pickup.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickup.SetActive(false);
            //textui.text = "Press 'E' to pick up " + transform.name;
        }
    }

}
