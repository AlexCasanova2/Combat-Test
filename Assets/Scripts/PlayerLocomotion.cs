using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    Animator animator;
    Vector2 input;
    public GameObject weapon;
    private Vector3 playerInput;
    public float playerSpeed = 1;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isCrouch", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("pickUpObject", false);
    }

    
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);

        playerInput = new Vector3(input.x, 0, input.y);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        animator.SetFloat("PlayerWalkVel", playerInput.magnitude * playerSpeed); ;

        Crouch();
        Run();
        //PickUpObject();
    }

    void Run(){

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void Crouch()
    { 
        
        if (Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("isCrouch",true);
        }
        else
        {
            animator.SetBool("isCrouch", false);
        }
       
        
    }



    /*private void OnTriggerStay(Collider other)
    {
        if (other.tag  == "Weapon" && Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("pickUpObject", true);
            Destroy(other.gameObject);
            
            weapon.SetActive(true);
            animator.SetBool("pickUpObject", false);
            animator.SetBool("GetSword", true);
        }
        else
        {
            animator.SetBool("pickUpObject", false);
        }
    }
    */


    /*void PickUpObject()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("pickUpObject", true);
        }
        else
        {
            animator.SetBool("pickUpObject", false);
        }
    }*/
}
