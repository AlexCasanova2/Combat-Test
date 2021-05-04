using UnityEngine.SceneManagement;
using UnityEngine;

public class CutSceneController : MonoBehaviour
{
    public bool isEnded;
    public void LoadMainScene()
    {
        isEnded = true;
        Debug.Log("Cargando...");
    }
}
