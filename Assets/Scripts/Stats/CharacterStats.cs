using System.Collections.Generic;
using Cinemachine;
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
    

    public bool _isdead;
    public bool isTalking;

    [Header("Sounds")]
    AudioSource audioSource;
    public AudioClip[] playerSounds;

    [Header("Stats")]
    public Stat damage;
    public Stat armor;

    [Header("Visual")]
    public GameObject gameManager;

    public Image healthUI;
    public ParticleSystem healing;

    public Image xpUI;
    public Text LvlUI;
    public ParticleSystem damageHit;

    [Header("Enemies")]
    public GameObject enemy;
    float _enemyDmg;
    private bool giveXp,_giveXp;
    

    #endregion

    private void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        currentHealth = maxHealth;
        _tInvulnerable = tInvulnerable;
        
        anims = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        _playerCamera = playerCamera.gameObject.GetComponent<CinemachineFreeLook>();

        LvlUI.text = playerLevel.ToString();
    }

    private void Update()
    {
        
        //Comprobamos si el jugador ha equipado un arma
        _isEquipped = gameManager.GetComponent<EquipmentManager>().isEquipped;

        //Comprobamos si el enemigo a muerto y nos da experiencia
        _giveXp = enemy.GetComponentInChildren<Enemy>().giveXP;

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
        
        //Si la experiencia es igual o mayor a 100 reseteamos la barra
        if (currentXp >= 100) GetXp(0);

        //Realizamos ataques con o sin combo
        AttackAndCombo();
        //Comprobamos si somos invulnerables
        Invulnerable();

        Debug.Log("giveXp: " + giveXp);
        if (giveXp)
        {
            Debug.Log("Give me xp");
            GetXp(10);
        }

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
            currentHealth += heal;
            healthUI.fillAmount += heal / 100f;
            healing.Play();
        }
    }

    public void GetXp(int xp)
    {
        
        if (currentXp >= 100)
        {
            playerLevel++;
            //Debug.Log("Subes de nivel. Ahora eres nivel: " + playerLevel);
            currentXp = 0;
            xpUI.fillAmount = 0;
            LvlUI.text = playerLevel.ToString();
        }
        currentXp += xp;
        Debug.Log(currentXp);
        xpUI.fillAmount += xp / 100f;
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking && isEquipped)
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
    }
}
