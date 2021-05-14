using UnityEngine.SceneManagement;
using UnityEngine;

public class CutSceneController : MonoBehaviour
{
    public bool isEnded;
    public GameObject timeline;
    public void LoadMainScene()
    {
        isEnded = true;
        Debug.Log("Cargando...");
    }
    public void HideText()
    {
        timeline.GetComponent<CutsceneEvents>().HideText();
    }
}
