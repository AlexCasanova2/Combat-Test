using UnityEngine;
using UnityEngine.UI;

public class AnimateEngine : MonoBehaviour
{
    Animator anim;
    AudioSource audioSource;
    public GameObject pickup;
    public Text textui;
    bool isActive;
    public bool isFinished;
    public int numTimes;

    [Header("GameController")]
    public GameObject gameController;

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
            textui.text = "Press 'E' to start " + transform.name;
        }
        if (Input.GetKeyDown(KeyCode.E) && other.CompareTag("Player"))
        {
            
            isActive = false;
            anim.SetBool("interacted", true);
            pickup.SetActive(isActive);
            audioSource.Play();
            gameObject.GetComponentInChildren<Outline>().enabled = false;
        }

    }
    public void IsFinished()
    {
        gameController.GetComponent<GameController>().totalEnginesCompleted++;
        isFinished = true;
        Destroy(audioSource);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickup.SetActive(false);
        }
    }
}
