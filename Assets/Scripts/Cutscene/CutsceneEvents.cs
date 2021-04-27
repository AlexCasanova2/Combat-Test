using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneEvents : MonoBehaviour
{   
    public GameObject canvas;

    [Header("Cameras")]
    public GameObject camera1;
    public GameObject camera2;
    public GameObject camera3;

    [Header("Text")]
    public GameObject title;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject text4;

    Animator animatorCameras;
    Animator animatorCanvas;
    public GameObject lightContainer;
    Light thunderLight;

    void Start()
    {
        animatorCanvas = canvas.GetComponent<Animator>();
        thunderLight = lightContainer.GetComponent<Light>();
    }

    void Update()
    {
        
    }


    public void ChangeToCameraTwo()
    {
        
        animatorCanvas.SetBool("isEnded", true);
        StartCoroutine(Example());
        
    }
    
    IEnumerator Example()
    {
        yield return new WaitForSeconds(3);
    }

    public void StartCameraTwo()
    {
        animatorCanvas.SetBool("isEnded", false);
        animatorCanvas.SetBool("beginsecond", true);
        camera1.SetActive(false);
        camera2.SetActive(true);
        lightContainer.SetActive(true);
        thunderLight.intensity = 2 ;
        
    }

    public void ChangeToCameraThree()
    {
        animatorCanvas.SetBool("beginsecond", false);
        animatorCanvas.SetBool("isEndedSecond", true);
        StartCoroutine(Example());
        
    }

    public void StartCameraThree()
    {
        animatorCanvas.SetBool("beginthird", true);
        camera2.SetActive(false);
        camera3.SetActive(true);
        
    }
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(3);
        
    }

    public void EndCameraThree()
    {
        //title.SetActive(true);
        animatorCanvas.SetBool("showTitle", true);
        StartCoroutine(LoadScene());
        
    }
    
}
