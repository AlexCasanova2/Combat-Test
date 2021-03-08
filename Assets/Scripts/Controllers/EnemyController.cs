using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;
    Animator animator;


    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        //animator = GetComponentInChildren<Animator>();
        animator = GetComponent<Animator>();
    }   

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        //Debug.Log(distance);
        if (distance <= lookRadius)
        {
            Debug.Log("ENTRA");
            agent.SetDestination(target.position);
            animator.SetBool("isWalking", true);

            if (distance <= agent.stoppingDistance)
            {
                Debug.Log("Dejo de andar");
                animator.SetBool("isWalking", false);
                //Attack
                Debug.Log("Ataco");
                animator.SetBool("isAttacking", true);
                FaceTarget();
            }
            else
            {
                Debug.Log("Dejo de atacar");
                animator.SetBool("isAttacking", false);
            }
        }
        else
        {
            //animator.SetBool("isWalking", false);
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
