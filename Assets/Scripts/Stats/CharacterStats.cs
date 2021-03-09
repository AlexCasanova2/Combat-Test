using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    Animator anims;

    CapsuleCollider coll;

    private int maxHealth = 100;
    public int currentHealth { get; private set; }

    public int combo;
    public float tCombo = 1.5f;
    public bool gotDMG, isAttacking, dead;

    private bool isEquipped, _isEquipped;

    public Stat damage;
    public Stat armor;

    [Header("Visual")]
    public GameObject gameManager;
    public Image healthUI;
    public ParticleSystem healing;
    public ParticleSystem damageHit;

    [Header("Enemies")]
    public GameObject enemy;
    float _enemyDmg;

    private void Awake()
    {
        currentHealth = maxHealth;
        anims = GetComponent<Animator>();
        coll = GetComponent<CapsuleCollider>();
       

       
    }


    private void Update()
    {
        //Comprobamos si el jugador ha equipado un arma
        _isEquipped = gameManager.GetComponent<EquipmentManager>().isEquipped;
       
        //Igualamos el valor de la variable
        isEquipped = _isEquipped;
        if (dead) { return; }
        if (gotDMG) { return; }

        
        //PRUEBAS
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            HealPlayer(5);
        }
        AttackAndCombo();


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
            Debug.Log(transform.name + " takes " + heal + " healing.");
            healthUI.fillAmount += heal / 100f;
            //Debug.Log(healthUI.fillAmount);
            healing.Play();
        }
    }


    public void TakeDamage(int damage)
    {
        //Restamos al daño el valor de la armadura
        damage -= armor.getValue();
        //Calculamos que si el valor de la armadura es mayor al daño el jugador no se cure.
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        
        Debug.Log(transform.name + " takes " + damage + " damage.");
        healthUI.fillAmount -= damage / 100f;
        //Debug.Log(healthUI.fillAmount);
        //gotDMG = true;
        anims.SetBool("GotHit", true);

        damageHit.Play();
        StopAttack();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void EndDMGAnim()
    {
        if (gotDMG)
        {
            gotDMG = false;
        }
    }

    public virtual void Die()
    {
        //Die
        //Este metodo sera sobrescrito
        Debug.Log(transform.name + " died");
        dead = true;
        //coll.direction = 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyWeapon"))
        {
            _enemyDmg = enemy.GetComponentInChildren<Enemy>().dmg;
            //Debug.Log(_enemyDmg);

            int enemyDmg = (int)_enemyDmg;
            TakeDamage(enemyDmg);
            //Invulnerable();
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
                //Debug.Log(isAttacking);
                Debug.Log("combo: " + combo);
                
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
   
    public void StopAttack()
    {
        if (isAttacking)
        {
            isAttacking = false;
        }
    }

    public void UpdateAnim()
    {
        anims.SetInteger("ACombo", combo);
        anims.SetBool("GotHit", gotDMG);
        anims.SetBool("Die", dead);
        anims.SetBool("isAttacking", isAttacking);
    }
}
