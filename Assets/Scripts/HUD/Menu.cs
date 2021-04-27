using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject menucontroller;
    AudioSource audioSource;
    public Image _arrow;
    Color newcolor;
    Color newcolor2;

    public Button yourButton;

    private void Start()
    {
        audioSource = menucontroller.GetComponent<AudioSource>();
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void Update()
    {
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.Play();

        newcolor = _arrow.color;
        newcolor.a = 1;
        _arrow.color = newcolor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        newcolor2 = _arrow.color;
        newcolor2.a = 0;
        _arrow.color = newcolor2;
    }

    void TaskOnClick()
    {
        //AudioSource audioSource = GetComponent<AudioSource>();
        //audioSource.PlayOneShot(check);
        //Debug.Log("You have clicked the button!");
    }

}
