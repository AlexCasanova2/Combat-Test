using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class prueba : MonoBehaviour
{

    //Animator anim;

    //[Header("HUD")]
    //public Slider slider;
    //public Image image;

    [Header("General")]
    public float invTime;
    //public int vida,vidaMax,vidaMin;
    //private float _vida;
    public bool invul, gotDmg, dead;
    private float _invTime;
    private float dmgRe;

    //[Header("Experiencia")]
    //public float exp;

    private void Awake()
    {
       // _invTime = invTime;
        //vida = 100;
        //_vida = vida;
    }

    void Start()
    {
        //anim = GetComponent<Animator>();
           
    }
    void Update()
    {
        Invulnerable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyWeapon"))
        {
            //GetDamaged();
            Invulnerable();
        }
    }

    void Invulnerable()
    {
        if (invul)
        {
            invTime -= Time.deltaTime;
            if (invTime <= 0)
            {
                invul = false;
                invTime = _invTime;
            }
        }
    }

    /*void GetDamaged()
    {
        dmgRe = 10;
        _vida -= dmgRe;
        Debug.Log(_vida);
        gotDmg = true;
        invul = true;
        
        //image.fillAmount -= dmgRe / 100;
        //Debug.Log(image.fillAmount);
        
        if (_vida <= 0)
        {
            dead = true;
            //Die();
        }
    }/*

    void StopDamaged() {
        gotDmg = false;
    }


    /*void Die()
    {
        anim.SetBool("Die", true);
        
       
    }*/


}
