using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Interactable focus;
    public bool isBlocking;
    public Animator animator;
    
    void Start()
    {
        //isBlocking = false;
    }

    private void Update()
    {
       /* if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }*/
    }

    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.Q))
        {
            animator.SetBool("isBlocking", true);
            isBlocking = true;
        }
        else
        {
            animator.SetBool("isBlocking", false);
            isBlocking = false;
        }
       

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            RemoveFocus();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Bit shift the index of the layer (8) to get a bit mask
            int layerMask = 1 << 8;

            // This would cast rays only against colliders in layer 8.
            // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
            layerMask = ~layerMask;

            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                //Debug.Log("Did Hit");

                Interactable interactable =  hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }

            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                //Debug.Log("Did not Hit");
            }
        }

        void SetFocus(Interactable newFocus)
        {
            if (newFocus != focus)
            {
                if (focus != null)
                {
                    focus.OnDefocused();
                    focus = newFocus;
                }
            }
            
            newFocus.OnFocused(transform);
        }

        void RemoveFocus()
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }

            
            focus = null;
        }

    }






}
