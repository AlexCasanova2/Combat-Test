using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    float waitForSeconds = 2f;
    [Header("Texto")]
    public Text text;
    [Header("Button")]
    public AudioClip check;
    public Button yourButton;

    float r = 255f, g = 255f, b = 255f, a = 1f;
    float _r = 255f, _g = 255f, _b = 255f, _a = 0.5f;

    public void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        text.color = new Color(r, g, b, a);
        
    }
    

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = new Color(_r, _g, _b, _a);
    }

    void TaskOnClick()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(check);
        Debug.Log("You have clicked the button!");
    }
}
