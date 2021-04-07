using UnityEngine;
using UnityEngine.UI;

public class AnimateObject : MonoBehaviour
{
    Animator anim;
    public GameObject pickup;
    public Text textui;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickup.SetActive(true);
            textui.text = "Press 'E' to open " + transform.name;
        }

        if (Input.GetKeyDown(KeyCode.E) && other.CompareTag("Player"))
        {
            anim.SetBool("OpenChest", true);
            pickup.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickup.SetActive(false);
        }
    }
}
