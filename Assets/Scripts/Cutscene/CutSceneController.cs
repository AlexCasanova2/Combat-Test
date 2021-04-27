using UnityEngine.SceneManagement;
using UnityEngine;

public class CutSceneController : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadSceneAsync(3);
    }
}
