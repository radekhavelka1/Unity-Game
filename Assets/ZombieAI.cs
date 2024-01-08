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

        if (distanceToTarget <= chaseRadius)
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
                // Player is within chase radius, run towards the player using NavMeshAgent
                navMeshAgent.SetDestination(Target.transform.position);
                transform.LookAt(Target.transform);

                // Play the run animation
                animator.SetBool("Idle", false);
                animator.SetBool("RUN", true);
            }
        }

        else
        {
            //Patrol jen kdy� chci aby hlidkoval zbytek kodu v tomto elsu smazat
            //Patrol();


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
        // Zkontrolujte, zda dos�hli aktu�ln�ho patroln�ho bodu
        if (Vector3.Distance(transform.position, patrolWaypoints[currentWaypointIndex].position) < 1.0f)
        {
            // P�ejd�te na dal�� bod v seznamu
            currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
        }

        // Nastavte destinaci zombika na aktu�ln� patroln� bod
        navMeshAgent.SetDestination(patrolWaypoints[currentWaypointIndex].position);

        // Oto�en� sm�rem k patroln�mu bodu
        Vector3 targetDirection = (patrolWaypoints[currentWaypointIndex].position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targetDirection.x, 0, targetDirection.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5.0f);  // �prava rychlosti ot��en�

        // Ov��te, zda zombik dos�hl patroln�ho bodu
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            // Zombik dos�hl patroln�ho bodu, tak�e m��ete zde prov�st n�jakou akci
            // Nap��klad m��ete zm�nit animaci nebo prov�st jin� �koly
        }

        // Nastavte animaci pro patrolov�n�
        animator.SetBool("Idle", false);
        animator.SetBool("RUN", true);
    }

}