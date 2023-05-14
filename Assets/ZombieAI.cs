using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    public GameObject Target;
    public float speed = 0.5f;

    public Transform groundCheck_Zombie;
    public float groundDistance = 0.5f;
    public LayerMask groundMask;

    public float detectionRadius = 5.0f;
    public LayerMask detectionMask;

    bool isGrounded;
    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {     
        isGrounded = Physics.CheckSphere(groundCheck_Zombie.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        /**
        transform.LookAt(Target.transform);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, detectionMask);
        foreach (Collider collider in hits)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                Target = collider.gameObject;
                transform.LookAt(Target.transform);
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
        }
        **/
    }


    // Draw the detection radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
