using UnityEngine;
using UnityEngine.UI;

public class AnimateObject : MonoBehaviour
{
    Animator anim;
    AudioSource audioSource;
    public GameObject pickup, playerPrefab;
    public Text textui;
    bool isActive;
    public int goldAmount;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        isActive = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickup.SetActive(isActive);
            textui.text = "Press 'E' to open " + transform.name;
        }

        if (Input.GetKeyDown(KeyCode.E) && other.CompareTag("Player"))
        {
            isActive = false;
            anim.SetBool("OpenChest", true);
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

    public void PlaySound()
    {
        audioSource.Play();
    }

    public void GiveGold()
    {
        playerPrefab.GetComponentInChildren<CharacterStats>().GetGold(goldAmount);
    }
}
