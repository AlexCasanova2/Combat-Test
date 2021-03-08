using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public int attackDamage = 5;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }   
    }


    void Attack()
    {
        //Play Attack animation
        bool haveSword = animator.GetBool("GetSword");
        if (haveSword)
        {
            animator.SetTrigger("Attack");
        }
        
        //Detect all the enemies

        //Damage enemies
    }
}
