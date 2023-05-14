using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    public GameObject Target;
    public float speed = 0.5f;
    public float detectionRadius = 5.0f;
    public float chaseRadius = 5.0f;

    public Transform groundCheck_Zombie;
    public float groundDistance = 0.5f;
    public LayerMask groundMask;
    public LayerMask detectionMask;

    private bool isGrounded;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        Debug.Log(distanceToTarget);
        Debug.Log(chaseRadius);
        if (distanceToTarget <= chaseRadius)
        {
            // Player is within chase radius, run towards the player
            Vector3 direction = (Target.transform.position - transform.position).normalized;
            rb.velocity = direction * speed;
            transform.LookAt(Target.transform);

        }
        else
        {
            Vector3 direction = (Target.transform.position - transform.position).normalized;
            rb.velocity = direction * 0;
            transform.LookAt(Target.transform);
        }


    }

    // Draw the detection radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
