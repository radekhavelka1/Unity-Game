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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        currentHealth = player.Health;
        healthBar.SetMaxHealth(currentHealth);
        UpdateHealthText();
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

    private void Attack()
    {
        // Apply damage to the target
        player.Health -= attackDamage;
        healthBar.SetHealth(player.Health);
        currentHealth = player.Health;
        if(currentHealth <= 0) 
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
}
