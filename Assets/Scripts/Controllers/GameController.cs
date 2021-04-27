using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;

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
    public Vector3 position;
    bool isFinished;

    public bool tutorialUI;
    public GameObject tutorialPopup;

    [Header("Engines Amount")]
    public bool canActivate;
    public int totalEnginesCompleted;

    [Header("Check Keys")]
    public bool haveKeys;

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

        if (Input.GetButtonDown("Inventory") && tutorialUI)
        {
            tutorialPopup.SetActive(false);
        }

        Dead();
    }

    public void ActivateDoor()
    {
        if(totalEnginesCompleted >= 3){
            canActivate = true;
        }
    }


    public void InstantiateEnemy()
    {

        for (int i = 0; i < numEnemies; i++)
        {
            GameObject var = Instantiate(enemyPrefab, position, Quaternion.identity);

            var.GetComponentInChildren<Enemy>().target = playerPrefab.transform.GetChild(1).gameObject;
            var.GetComponentInChildren<Enemy>().activeState = Enemy.States.chase;

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
