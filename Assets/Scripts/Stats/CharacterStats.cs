using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class CharacterStats : MonoBehaviour
{
    #region Variables
    Animator anims;

    private int maxHealth = 100;
    [Header("Camera")]
    public CinemachineFreeLook playerCamera;
    CinemachineFreeLook _playerCamera;

    public int currentHealth { get; private set; }
    [Header("Player Combat")]
    public int combo;
    public float tCombo = 1.5f;
    public bool gotDMG, isAttacking, dead;
    public bool isVulnerable;
    public float tInvulnerable = 1.5f;
    private float _tInvulnerable;
    private bool isEquipped, _isEquipped;
    private int playerLevel = 1;
    private int currentXp;
    private int currentGold = 0;
    private string _currentGold;
    public bool isBlocking;

    public bool _isdead;
    public bool isTalking;
    //Escalar
    public bool _canClimb;

    [Header("Sounds")]
    AudioSource audioSource;
    public AudioClip[] playerSounds;
    public AudioClip[] footSteps;
    public AudioClip[] attackSounds;

    [Header("Stats")]
    public Stat damage;
    public Stat armor;
    public int gold;

    [Header("Visual")]
    public GameObject gameManager;

    public Image healthUI;
    public ParticleSystem healing;

    public Image xpUI;
    public Text LvlUI;
    public ParticleSystem damageHit;

    [Header("Experience")]
    public GameObject getxpText;
    Animator gettingxp;
    public TextMeshProUGUI text;

    [Header("Gold")]
    public GameObject getGoldText;
    Animator gettingGold;
    public TextMeshProUGUI goldText;

    public TextMeshProUGUI currentGoldText;


    [Header("Enemies")]
    public GameObject enemy;
    float _enemyDmg;
    private bool giveXp,_giveXp;

    //Listado de enemigos con los que hemos interactuado
    public List<GameObject> ListadoEnemigos;

    #endregion

    private void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        currentHealth = maxHealth;
        _tInvulnerable = tInvulnerable;
        
        anims = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        _playerCamera = playerCamera.gameObject.GetComponent<CinemachineFreeLook>();
        //xpreceived = enemy.GetComponentInChildren<Enemy>().xpToGive;

        gettingxp = getxpText.GetComponent<Animator>();
        gettingGold = getGoldText.GetComponent<Animator>();

        LvlUI.text = playerLevel.ToString();
    }

    private void Update()
    {
        //Comprobamos si el jugador ha equipado un arma
        _isEquipped = gameManager.GetComponent<EquipmentManager>().isEquipped;

        //Igualamos el valor de la variable
        isEquipped = _isEquipped;
        giveXp = _giveXp;

        
        if (dead) { return; }
        if (gotDMG) { return; }
        if (isTalking) { return; }

        //PRUEBAS
        if (Input.GetKeyDown(KeyCode.T)) TakeDamage(10);
        if (Input.GetKeyDown(KeyCode.H)) HealPlayer(5);
        if (Input.GetKeyDown(KeyCode.X)) GetXp(10);

        //Realizamos ataques con o sin combo
        AttackAndCombo();
        //Comprobamos si somos invulnerables
        Invulnerable();
        //Hacemos Roll
        Roll();
        Block();
        Climb();

        //Oro actual
        _currentGold = currentGold.ToString();
        currentGoldText.SetText(_currentGold);

    }
    private void LateUpdate()
    {
        UpdateAnim();
    }

    public void HealPlayer(int heal)
    {
        if (currentHealth >= 100)
        {
            Debug.Log("No te puedes curar, tienes: " + currentHealth + " de vida.");
        }
        else
        {
            //Play al sonido
            audioSource.PlayOneShot(playerSounds[2], 0.7f);
            currentHealth += heal;
            healthUI.fillAmount += heal / 100f;
            healing.Play();
        }
    }

    public void AddEnemyToList(GameObject enemyGameObject)
    {
        bool alreadyExist = ListadoEnemigos.Contains(enemyGameObject);
        if (!alreadyExist)
        {
            ListadoEnemigos.Add(enemyGameObject);
            //Debug.Log("Añado el GameObject");
        }
       
    }
    public void DeleteEnemyToList(GameObject deleteGameObject)
    {
        ListadoEnemigos.Remove(deleteGameObject);
        //Debug.Log("Elimino el GameObject");
    }

    public void GetXp(int xp)
    {
        //Debug.Log("xpreceived: " + xpreceived);

        if (currentXp >= 100)
        {
            playerLevel++;
            //Debug.Log("Subes de nivel. Ahora eres nivel: " + playerLevel);
            currentXp = 0;
            xpUI.fillAmount = 0;
            LvlUI.text = playerLevel.ToString();
        }
        //havexp = true;
        gettingxp.SetBool("haveXp", true);
        text.SetText("+" + xp + " xp");

        currentXp += xp;
        //Debug.Log(currentXp);
        xpUI.fillAmount += xp / 100f;
    }

    public void GetGold(int gold)
    {
        //Notification Text
        currentGold += gold;
        gettingGold.SetBool("haveGold", true);
        goldText.SetText("+ " + gold + " gold");

        //UI Text
        _currentGold = currentGold.ToString();
        currentGoldText.SetText(_currentGold);
    }


    public void TakeDamage(int damage)
    {
        //Play al sonido
        audioSource.PlayOneShot(playerSounds[0], 1f);
        //Restamos al daño el valor de la armadura
        damage -= armor.getValue();
        //Calculamos que si el valor de la armadura es mayor al daño el jugador no se cure.
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;

        healthUI.fillAmount -= damage / 100f;
        //gotDMG = true;
        anims.SetBool("GotHit", true);
        //Nos hacemos invulnerables
        isVulnerable = true;
        damageHit.Play();
        StopAttack();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void EndDMGAnim()
    {
        if (gotDMG) gotDMG = false;
    }
    

    public virtual void Die()
    {
        //Play al sonido
        audioSource.PlayOneShot(playerSounds[1], 0.7f);
        dead = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //añadir si no estoy bloqueando
        if (other.CompareTag("EnemyWeapon"))
        {
            _enemyDmg = enemy.GetComponentInChildren<Enemy>().dmg;
            int enemyDmg = (int)_enemyDmg;
            TakeDamage(enemyDmg);
        }
    }

    void AttackAndCombo()
    {
        //Si apretamos el boton de accion, no estamos atacando y tenemos equipada un arma empezamos el combo
        if (Input.GetButtonDown("Attack") && !isAttacking && isEquipped)
        {
            if (combo < 3)
            {
                combo++;
                isAttacking = true;
                tCombo = 1.5f;
            }
        }
        if (combo > 0)
        {
            tCombo -= Time.deltaTime;
            if (tCombo <= 0f)
            { 
                if (combo == 3 || combo == 2)
                {
                    combo = 0;
                }
                else
                {
                    combo--;
                }
                tCombo = 1.5f;
            }
        }
    }

    public void Roll()
    {
        if (Input.GetButtonDown("Roll"))
        {
            anims.SetBool("Roll", true);
        }
    }

    public void Climb()
    {
        if (Input.GetKeyDown(KeyCode.Y) && _canClimb)
        {
            anims.SetBool("Climb", true);
        }
        
    }
    

    public void Block()
    {
        if (Input.GetButton("Block"))
        {
            isBlocking = true;
        }
        else{
            isBlocking = false;
        }
    }

    public void Invulnerable()
    {
        if (isVulnerable)
        {
            tInvulnerable -= Time.deltaTime;
            if (tInvulnerable <= 0)
            {
                isVulnerable = false;
                tInvulnerable = _tInvulnerable;
            }
        }
    }

    public void Cinematica()
    {
        if (isTalking)
        {
            isVulnerable = true;
            GetComponent<ThirdPersonCharacter>().enabled = false;
            GetComponent<ThirdPersonUserControl>().enabled = false;
            _playerCamera.enabled = false;
        }
    }

    public void StopAttack()
    {
        if (isAttacking) isAttacking = false;
    }

    public void UpdateAnim()
    {
        anims.SetInteger("ACombo", combo);
        anims.SetBool("GotHit", gotDMG);
        anims.SetBool("Die", dead);
        anims.SetBool("isAttacking", isAttacking);
        anims.SetBool("isBlocking", isBlocking);
        //gettingxp.SetBool("haveXp", havexp);
    }

    public void Step()
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }
    private AudioClip GetRandomClip()
    {
        return footSteps[UnityEngine.Random.Range(0, footSteps.Length)];
    }

    public void PlayerAttack01()
    {
        //Play al sonido
        audioSource.PlayOneShot(attackSounds[0], 0.7f);
    }
    public void PlayerAttack02()
    {
        //Play al sonido
        audioSource.PlayOneShot(attackSounds[1], 0.7f);
    }
    public void PlayerAttack03()
    {
        //Play al sonido
        audioSource.PlayOneShot(attackSounds[2], 0.7f);
    }
    public void RollingSound()
    {
        audioSource.PlayOneShot(playerSounds[3], 1f);
    }
    public void RollingSound2()
    {
        audioSource.PlayOneShot(playerSounds[4], 0.3f);
    }
}
