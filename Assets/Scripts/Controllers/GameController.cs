using Cinemachine;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public CinemachineFreeLook cameraLook;
    CinemachineFreeLook hola;
    public GameObject pauseMenu;
    public GameObject deadMenu;

    public static bool GameIsPaused = false;

    public GameObject playerPrefab;
    bool _playerDead;
    bool _giveXp;
    public GameObject enemyPrefab, dialogueManager;
    public int numEnemies;
    bool isFinished;

    void Start()
    {
        hola = cameraLook.gameObject.GetComponent<CinemachineFreeLook>();
    }

    private void Update()
    {
        //Check if player is dead
        _playerDead = playerPrefab.GetComponentInChildren<CharacterStats>().dead;

        if (isFinished) { return; }
        isFinished = dialogueManager.GetComponent<DialogueManager>().isFinished;
        StopMovement();
       
        if (Input.GetButtonDown("Cancel"))
        {
            if (GameIsPaused) Resume();
            else Pause();
        }

        Dead();
    }

    public void InstantiateEnemy()
    {

        for (int i = 0; i < numEnemies; i++)
        {
            Instantiate(enemyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            isFinished = false;
        }
    }

    void Dead()
    {
        if (_playerDead)
        {
            deadMenu.SetActive(true);
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
        if (Input.GetButtonDown("Cancel"))
        {
            UnityEngine.Cursor.visible = ! UnityEngine.Cursor.visible;
            
            hola.enabled = ! hola.enabled;
        }
    }
}
