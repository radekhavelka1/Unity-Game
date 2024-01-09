using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ZombieAI : MonoBehaviour
{
    public GameObject Target;
    public float speed = 0.5f;
    public float detectionRadius = 5.0f;
    public float chaseRadius = 5.0f;
    public float attackRadius = 2.0f;
    public PlayerMovement player;
    public HealthBar healthBar;

    public Transform groundCheck_Zombie;
    public float groundDistance = 0.5f;
    public LayerMask groundMask;
    public LayerMask detectionMask;

    private bool isGrounded;
    private Rigidbody rb;
    private Animator animator;
    public Text healthText;
    private int currentHealth;

    public bool idle = true;
    public bool run = false;

    private float attackPeriod = 2f;
    private int attackDamage = 10;

    private float timeSinceLastAttack = 0f;

    public int zombieHealth = 100;

    private UnityEngine.AI.NavMeshAgent navMeshAgent;


    public Transform[] patrolWaypoints;
    private int currentWaypointIndex = 0;


    private Vector3 currentTargetPosition;

    private bool chasing;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        currentHealth = player.Health;
        healthBar.SetMaxHealth(currentHealth);
        UpdateHealthText();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();



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

        /*
        float angleToPlayer = Vector3.Angle(transform.forward, (Target.transform.position - transform.position).normalized);

        if(angleToPlayer > 150.0f)
        {
            chaseRadius = 3.0f;
        }
        else
        {
            chaseRadius = 5.0f;
        }

        */


        if (distanceToTarget <= chaseRadius )
        {


            if (distanceToTarget <= attackRadius)
            {
                // Stop moving and play the attack animation
                rb.velocity = Vector3.zero;
                transform.LookAt(Target.transform);

                animator.SetTrigger("Attack");
                animator.SetBool("Idle", false);
                animator.SetBool("RUN", false);

                timeSinceLastAttack += Time.deltaTime;

                if (timeSinceLastAttack >= attackPeriod)
                {
                    Attack();
                    Debug.Log(player.Health);
                    timeSinceLastAttack = 0f;
                }
            }

            else
            {
                transform.LookAt(Target.transform);
                if (Vector3.Distance(currentTargetPosition, Target.transform.position) > 1.0f)
                {
                    currentTargetPosition = Target.transform.position;
                    navMeshAgent.SetDestination(currentTargetPosition);
                    
                }
               
                // Play the run animation
                animator.SetBool("Idle", false);
                animator.SetBool("RUN", true);
            }
        }

        else
        {
            Patrol();

        }
    }

    // Draw the detection radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void Attack()
    {
        // Apply damage to the target
        player.Health -= attackDamage;
        healthBar.SetHealth(player.Health);
        currentHealth = player.Health;
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        // Update the text object with the player's current health
        healthText.text = "HP: " + currentHealth.ToString();
    }


    private void Patrol()
    {

        // Zkontrolujte, zda dosáhli aktuálního patrolního bodu
        if (Vector3.Distance(transform.position, patrolWaypoints[currentWaypointIndex].position) < 1.0f)
        {
            // Pøejdìte na další bod v seznamu
            currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
        }

        // Nastavte destinaci zombika na aktuální patrolní bod
        navMeshAgent.SetDestination(patrolWaypoints[currentWaypointIndex].position);

        // Otoèení smìrem k patrolnímu bodu
        Vector3 targetDirection = (patrolWaypoints[currentWaypointIndex].position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targetDirection.x, 0, targetDirection.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5.0f);  // Úprava rychlosti otáèení
        transform.LookAt(patrolWaypoints[currentWaypointIndex].position);

        // Ovìøte, zda zombik dosáhl patrolního bodu
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            // Zombik dosáhl patrolního bodu, takže mùžete zde provést nìjakou akci
            // Napøíklad mùžete zmìnit animaci nebo provést jiné úkoly
        }

        // Nastavte animaci pro patrolování
        animator.SetBool("Idle", false);
        animator.SetBool("RUN", true);
    }

}