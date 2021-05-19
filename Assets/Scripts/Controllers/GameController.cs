using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using TMPro;

public class GameController : MonoBehaviour
{
    public CinemachineFreeLook cameraLook;
    CinemachineFreeLook hola;
    public GameObject pauseMenu;
    public GameObject deadMenu;

    public static bool GameIsPaused = false;
    public static bool controlHUD = false;
    public static bool escapePressed = false;
    bool inventoryPressed;

    public GameObject playerPrefab, uiTutorial;
    int playerCurrentHelath;
    int timesEntered;
    public TextMeshProUGUI textTutorial;
    bool lowHealth;
    bool _playerDead;
    bool _giveXp;
    public GameObject enemyPrefab, dialogueManager;
    public int numEnemies;
    public Vector3 position;
    bool isFinished;

    //Cursor lock
    bool cursorLockedVar;

    public bool tutorialUI;
    public GameObject tutorialPopup;

    [Header("Engines Amount")]
    public bool canActivate;
    public int totalEnginesCompleted;

    [Header("Check Keys")]
    public bool haveKeys;


    private void Awake()
    {
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start()
    {
        hola = cameraLook.gameObject.GetComponent<CinemachineFreeLook>();
        
    }

    private void Update()
    {
        //Check if player is dead
        _playerDead = playerPrefab.GetComponentInChildren<CharacterStats>().dead;
        playerCurrentHelath = playerPrefab.GetComponentInChildren<CharacterStats>().currentHealth;

        if (isFinished) { return; }
        isFinished = dialogueManager.GetComponent<DialogueManager>().isFinished;

        inventoryPressed = InventoryUI.inventoryPressed;
       
        if (Input.GetButtonDown("Cancel"))
        {
            if (controlHUD && GameIsPaused)
            {
                pauseMenu.SetActive(false);
                PauseYMouseControl(false, false);
                escapePressed = false;
            }
            else
            {
                PauseYMouseControl(true, true); pauseMenu.SetActive(true);
                escapePressed = true;
            }
        }

        if (Input.GetButtonDown("Inventory") && tutorialUI)
        {
            tutorialPopup.SetActive(false);
        }

        Dead();
        ShowTutorialHeal();
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
            uiTutorial.SetActive(false);
            GameController.PauseYMouseControl(false,true);
        }
    }

    public static void PauseYMouseControl( bool pauseB, bool mouseB)
    {
        controlHUD = mouseB;
        GameIsPaused = pauseB;

        if (GameIsPaused)
        {
            controlHUD = true;
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if (controlHUD && !GameIsPaused)
        {
            Time.timeScale = 1;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void StopMovement()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Cursor.visible = !Cursor.visible;
            hola.enabled = ! hola.enabled;
        }
    }

    public void ShowTutorialHeal()
    {
        if (timesEntered <= 1)
        {
            if (playerCurrentHelath == 60)
            {
                lowHealth = true;
                uiTutorial.SetActive(true);
                textTutorial.SetText("Press 'H' to heal");
                timesEntered++;
            }
        }
        else
        {

        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            uiTutorial.SetActive(false);
        }

    }
}
