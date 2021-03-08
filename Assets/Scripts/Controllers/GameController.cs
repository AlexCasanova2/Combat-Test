using Cinemachine;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public CinemachineFreeLook cameraLook;
    CinemachineFreeLook hola;
    public GameObject pauseMenu;

    public static bool GameIsPaused = false;

    void Start()
    {
        UnityEngine.Cursor.visible = false;
        hola = cameraLook.gameObject.GetComponent<CinemachineFreeLook>();
       
    }

    private void Update()
    {
        StopMovement();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
    }

    void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }


    public void StopMovement()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UnityEngine.Cursor.visible = ! UnityEngine.Cursor.visible;
            
            hola.enabled = ! hola.enabled;
        }
        
    }


}
