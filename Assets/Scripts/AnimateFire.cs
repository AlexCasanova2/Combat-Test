using UnityEngine;
using UnityEngine.UI;

public class AnimateFire : MonoBehaviour
{
    public ParticleSystem fireParticle;
    public GameObject pickup;
    public Text textui;
    bool isActive;

    void Start()
    {
        isActive = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickup.SetActive(isActive);
            textui.text = "Press 'E' to start " + transform.name;
        }
        if (Input.GetKeyDown(KeyCode.E) && other.CompareTag("Player"))
        {
            fireParticle.Play();
            isActive = false;
            pickup.SetActive(isActive);
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
