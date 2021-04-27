using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PreviousScene : MonoBehaviour
{
    Animator anim;
    AudioSource audioSource;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            anim.SetBool("isClicked", true);
            audioSource.Play();
            StartCoroutine(LoadScene());
            
        }   
    }
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadSceneAsync(1);
    }
}
