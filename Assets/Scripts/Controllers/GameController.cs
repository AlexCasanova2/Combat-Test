using Cinemachine;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public CinemachineFreeLook cameraLook;
    CinemachineFreeLook hola;
    public GameObject pauseMenu;

    public static bool GameIsPaused = false;

    public GameObject enemyPrefab, dialogueManager;
    public int numEnemies;
    bool isFinished;

    void Start()
    {
        hola = cameraLook.gameObject.GetComponent<CinemachineFreeLook>();
    }

    private void Update()
    {
        if (isFinished) { return; }
        isFinished = dialogueManager.GetComponent<DialogueManager>().isFinished;
        StopMovement();
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) Resume();
            else Pause();
        }
    }

    public void InstantiateEnemy()
    {

        for (int i = 0; i < numEnemies; i++)
        {
            Instantiate(enemyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            isFinished = false;
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
