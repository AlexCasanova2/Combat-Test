using System;
using UnityEngine;
using UnityEngine.AI;   

public class Enemy : MonoBehaviour
{
    public enum States {move, chase, attack, dead, gotdmg, wait}
    public States activeState = States.wait;

    Animator anim;
    NavMeshAgent agent;

    Vector3 activeDestination;
    

    public bool dead;
    public float dmg;
    public float _vida;
    private float vidaMax = 100, vidaMin = 0;
    public bool giveXP;
    public int xpToGive;

    [Header("Player")]
    public GameObject target = null;    
    int _playerDmg;
    Stat playerDamage;
    bool addGameobject;

    public float vida {
        get {
            return _vida; //return nos devolverá el valor que le indiquemos
        }
        set {
            if (value <= vidaMin) //Si la vida es menor a la vida minima
            {
                _vida = vidaMin;
            }
            else if(value >= vidaMax){ //Si la vida es mayor que la vida maxima
                _vida = vidaMax;
            }
            else
            {
                _vida = value; //value sera el valor que indiquemos al declarar el valor del parametro
            }
        }
    }

    public float wanderRadius;
    public float wanderTimer, waitTimer, attackTimer;
    private float timer, wTimer, aTimer;

    RaycastHit hit;

    public bool inv;
    public float lookRadius = 10f;

    public ParticleSystem damageHit;

    [Header("Lista de audio")]
    AudioSource audioSource;
    public AudioClip[] audioClips;

    private void Awake()
    {
        //xpToGive = 10;
        giveXP = false;
    }
    void Start()
    {
        vida = vidaMax;
        timer = wanderTimer;
        wTimer = 0;
        
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        if (giveXP) return;
        if (addGameobject)
        {
            //player.GetComponentInChildren<CharacterStats>().AddEnemyToList(gameObject);
        }
        switch (activeState)
        {
            case States.wait:

                break;

            case States.move:
                MoveAgent();

                break;

            case States.chase:
                if (target == null)
                {
                    activeState = States.move;
                    transform.rotation *= Quaternion.Euler(0, -1, 0);
                    return;
                }

                activeDestination = target.transform.position;
                //float distance = Vector3.Distance(target.transform.position, transform.position);
                if (Vector3.Distance(target.transform.position, transform.position) > 1.5f)
                {
                    agent.SetDestination(activeDestination);
                    Vector3 LookPos = new Vector3(activeDestination.x, transform.position.y, activeDestination.z);
                    transform.LookAt(LookPos);
                }
                else if (Vector3.Distance(target.transform.position, transform.position) > 15f)
                {
                    activeState = States.move;
                    target = null;
                }
                else
                {
                    activeState = States.attack;                  
                }

                break;

            case States.attack:
                
                if (Vector3.Distance(target.transform.position, transform.position) > 1.5f)
                {
                    activeState = States.chase;
                }
                if (target.GetComponent<CharacterStats>().dead)
                {
                    activeState = States.wait;
                }
                
                break;

            case States.gotdmg:

                anim.SetBool("GotDMG", true);
                if (!inv)
                {
                    activeState = States.chase;
                }

                break;

            case States.dead:

                transform.GetComponent<NavMeshAgent>().enabled = false;
                transform.GetComponent<CapsuleCollider>().enabled = false;
                Die();
                
                break;
        }
    }


    private void LateUpdate()
    {
        switch (activeState)
        {
            case States.wait:

                wTimer += Time.deltaTime;

                anim.SetBool("Idle", true);

                if (wTimer >= waitTimer)
                {
                    wTimer = 0;
                    timer = wanderTimer;
                    MoveAgent();
                }
                break;

            case States.move:

                anim.SetBool("Walk", true);
                anim.SetBool("Idle", false);
                if (Vector3.Distance(activeDestination, transform.position) <= 1f)
                {
                    anim.SetBool("Walk", false);
                    anim.SetBool("Idle", true);
                    activeState = States.wait;
                    timer = 0;
                }

                break;

            case States.chase:

                if (target == null)
                {
                    return;
                }

                if (Vector3.Distance(target.transform.position, transform.position) > 1.5f)
                {
                    anim.SetBool("Walk", true);
                    anim.SetBool("Idle", false);
                }
                else
                {
                    anim.SetBool("Walk", false);
                    anim.SetBool("Idle", true);
                }
                break;

            case States.attack:

                aTimer += Time.deltaTime;

                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);
                anim.SetBool("GotDMG", false);
                anim.SetBool("Attack", false);
                
                if (aTimer >= attackTimer)
                {
                    anim.SetBool("Attack", true);
                    aTimer = 0;
                    
                }
                break;

            case States.gotdmg:

                anim.SetBool("Walk", false);
                anim.SetBool("Idle", false);
                anim.SetBool("Attack", false);

                anim.SetBool("GotDMG", true);

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Get Hit"))
                {
                    anim.SetBool("GotDMG", false);
                }
                break;

            case States.dead:

                anim.SetBool("Attack", false);
                anim.SetBool("Idle", false);
                anim.SetBool("GotDMG", false);
                anim.SetBool("Walk", false);


                anim.SetBool("Dead", true);
                
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
                {
                    anim.SetBool("Dead", false);
                }

                break;
        }
        
    }

    private void FixedUpdate()
    {
        if (SeePlayer().b && activeState != States.dead)
        {
            if (SeePlayer().g != null)
            {
                if (SeePlayer().g.GetComponent<CharacterStats>().dead)
                {
                    activeState = States.move;
                }
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Get Hit"))
                {
                    target = SeePlayer().g;
                    activeState = States.chase;
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {    
        if (other.CompareTag("PlayerWeapon") && !inv)
        {
            try
            {
                playerDamage = target.GetComponent<CharacterStats>().damage;
                target.GetComponent<CharacterStats>().AddEnemyToList(gameObject);
                _playerDmg = playerDamage.getValue();
            }
            catch (NullReferenceException ex)
            {
                Debug.LogWarning("No existe el componente.");
            }
            TakeDamage(_playerDmg);
            addGameobject = true;
            
        }
    }
    
    public virtual void Die()
    {
        giveXP = true;
        dead = true;
        Destroy(transform.parent.gameObject, 5f);
        Destroy(gameObject, 5f);
        
        target.GetComponent<CharacterStats>().DeleteEnemyToList(gameObject);
        
    }
    public void GiveXPToPlayer()
    {
        if (giveXP) target.GetComponent<CharacterStats>().GetXp(xpToGive);
    }
    public void PlayDeadSound()
    {
        //Play Sound
        audioSource.PlayOneShot(audioClips[1], 0.4f);
    }
    public void PlayAttackSound()
    {
        audioSource.PlayOneShot(audioClips[2], 0.4f);
    }

    public void TakeDamage(int damage)
    {
        //Play sound
        audioSource.PlayOneShot(audioClips[0], 0.4f);
        audioSource.PlayOneShot(audioClips[3], 0.4f);
        activeState = States.gotdmg;
        damageHit.Play();
        //DamagePopup.Create(transform.position, (int)p.dmg, p.combo == 3);

        vida -= damage;
        //inv = true;

        if (vida <= 0)
        {
            activeState = States.dead;
        }
    }

    public void MoveAgent()
    {
        timer += Time.deltaTime;
        activeState = States.move;
        if (timer >= wanderTimer)
        {
            activeDestination = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(activeDestination);
            timer = 0;
        }
    }

    public void EndGotDMG()
    {
        inv = false;
        anim.SetBool("GotDMG", false);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    public Eyes SeePlayer()
    {
        if (Physics.BoxCast(transform.position + new Vector3(0, 1, 0), new Vector3(1, 1, 1), transform.forward, out hit, Quaternion.identity, 10))
        {
            if (hit.transform.CompareTag("Player"))
            {
                return new Eyes() { g = hit.transform.gameObject, b = true };
            }
            return new Eyes() { g = null, b = false };
        }
        else { return new Eyes() { g = null, b = false }; }
    }
    public class Eyes
    {
        public GameObject g;
        public bool b;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + new Vector3(0, 1, 0) + transform.forward * hit.distance, new Vector3(1, 1, 1));
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void UpdateAnim()
    {
        anim.SetBool("Dead", dead);
    }

    public void StopAction()
    {
        anim.SetBool("Dead", false);
    }
    

}
