using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    public GameObject Target;
    public float speed = 1.5f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;
    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundDistance, groundMask))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y + groundDistance, transform.position.z);
        }

        transform.LookAt(Target.gameObject.transform);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
