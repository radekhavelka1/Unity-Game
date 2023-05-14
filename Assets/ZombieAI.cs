using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    public GameObject Target;
    public float speed = 0.5f;
    public float detectionRadius = 5.0f;
    public float chaseRadius = 5.0f;
    public float attackRadius = 2.0f;

    public Transform groundCheck_Zombie;
    public float groundDistance = 0.5f;
    public LayerMask groundMask;
    public LayerMask detectionMask;

    private bool isGrounded;
    private Rigidbody rb;
    private Animator animator;


    private bool isIdle = true;
    private bool isRunning = false;

    public bool idle = true;
    public bool run = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck_Zombie.position, groundDistance, groundMask);

        if (!isGrounded)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, groundMask))
            {
                transform.position = hit.point + Vector3.up * groundDistance;
            }
        }

        float distanceToTarget = Vector3.Distance(transform.position, Target.transform.position);

        if (distanceToTarget <= chaseRadius)
        {

            if (distanceToTarget <= attackRadius)
            {
                // Stop moving and play the idle animation
                rb.velocity = Vector3.zero;
                transform.LookAt(Target.transform);

                animator.SetTrigger("Attack");
                animator.SetBool("Idle", false);
                animator.SetBool("RUN", false);
            }

            else
            {
                // Player is within chase radius, run towards the player
                Vector3 direction = (Target.transform.position - transform.position).normalized;
                rb.velocity = direction * speed;
                transform.LookAt(Target.transform);

                // Play the run animation
                animator.SetBool("Idle", false);
                animator.SetBool("RUN", true);
            }
        }

        else
        {
            // Stop moving and play the idle animation
            rb.velocity = Vector3.zero;
            transform.LookAt(Target.transform);

            // Play the idle animation
            animator.SetBool("Idle", true);
            animator.SetBool("RUN", false);
        }
    }

    // Draw the detection radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
