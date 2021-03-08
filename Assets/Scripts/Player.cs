using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player main;

    CharacterController character;
    public float speed,turnSpeed, jumpSpeed, baseDMG;

    public float dmg {
        get {
            if (combo == 2)
            {
                return baseDMG * 1.5f;
            }
            else if(combo == 3){
                return baseDMG * 2f;
            }
            return baseDMG;
        }
    }

    Vector3 dir;


    public Animator[] anims;

    public int combo;
    public float tCombo = 1.5f;
    public bool isAttacking, jump;

    public bool gotDmg, invul, dead;

    private float _vida, _invT;
    public float vidaMax, vidaMin, invT;

    public float vida {
        get {
            return _vida; //return nos devolvera el valor que le indiquemos
        }
        set {
            if (value < vidaMin) //Si es menor que la vida minima
            {
                _vida = vidaMin;
            }
            else if (value <= vidaMax)
            {
                _vida = vidaMax;
            }
            else
            {
                _vida = value;
            }
        }
    }

    public List<Transform> lEnemiesGo;
    public int fixedEnemy;
    public bool focusEnemy;


    private void Awake()
    {
        if (main == null)
        {
            main = this;
        }

        //cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        //camOffset = cam.position - transform.position;

        anims = GetComponentsInChildren<Animator>();
        character = GetComponent<CharacterController>();
        vida = vidaMax;
        _invT = invT;
        lEnemiesGo = new List<Transform>();
    }


    void Update()
    {
        if (dead){ return; }
        //Movement();
        //AttackCombo();

        //Invulnerable();
        
    }

    private void LateUpdate()
    {
       // UpdateAnim();
    }

    private void FixedUpdate()
    {
        if (focusEnemy || jump || dead){ return; }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyWeapon"))
        {
            //ReceiveDMG(other.GetComponentInParent<Enemy>().dmg);
            Debug.Log("OUCH");
           
            if (jump && !invul)
            {
                
                
            }
        }
    }

    /*void Invulnerable()
    {
        if (invul)
        {
            invT -= Time.deltaTime;
            if (invT <= 0)
            {
                invul = false;
                invT = _invT;
            }
        }
    }*/

    /*public void ReceiveDMG(float x)
    {
        vida -= x;
        vida -= 10;
        gotDmg = true;
        invul = true;
        Debug.Log(vida);
        StopAttack();
        character.Move(-transform.forward * 10 * Time.deltaTime);
        if (vida <= 0)
        {
            dead = true;
        }
    }*/

    public void EndDMGAnim()
    {
        if (gotDmg)
        {
            gotDmg = false;
        }
    }

    public void StopAttack()
    {
        if (isAttacking)
        {
            isAttacking = false;
        }
    }

    /*void Movement()
    {
        if (gotDmg) { return; }

        if (character.isGrounded)
        {
            dir = (transform.right * Input.GetAxis("Horizontal")) + (transform.forward * Input.GetAxis("Vertical"));
            dir *= speed;
            // Debug.Log(dir);
            if (Input.GetAxis("Vertical") > 0.9f && Input.GetKey(KeyCode.LeftShift))
            {
                dir *= 1.5f;
            }
            //transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal"), 0) * Time.deltaTime * turnSpeed);           
        }
        if (isAttacking && !jump)
        {
            dir = transform.forward + transform.up * dir.y;
        }

        dir.y -= 9.81f * Time.deltaTime;

        if (Input.GetButton("Jump") && !jump && !isAttacking && character.isGrounded)
        {
            if (focusEnemy) { focusEnemy = false; }
            if (Input.GetAxis("Vertical") < 0)
            {
                dir = (-transform.forward * jumpSpeed * 1.5f) + (transform.up * jumpSpeed);
                transform.forward = -transform.forward;
            }
            else
            {
                dir = (transform.forward * jumpSpeed * 1.5f) + (transform.up * jumpSpeed);
            }
            // character.height = 1f;
            jump = true;
        }

        character.Move(dir * Time.deltaTime);
    }*/

    /*void Cam()
    {
        cam.position = Vector3.Lerp(cam.position, transform.position + camOffset, camTime * Time.deltaTime);
    }*/

    void UpdateAnim()
    {
        float pSpeed = Input.GetAxis("Vertical");
        if (Input.GetAxis("Vertical") > 0.9f && Input.GetKey(KeyCode.LeftShift))
        {
            pSpeed = 1.5f;
        }

        foreach (Animator a in anims)
        {
            a.SetFloat("PSpeed", pSpeed);
            a.SetBool("Attack", isAttacking);
            a.SetBool("GotHit", gotDmg);
            a.SetBool("Dead", dead);
            
        }
    }

}
