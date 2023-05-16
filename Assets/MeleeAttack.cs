using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MeleeAttack : MonoBehaviour
{
    private Animator animator;

    public ZombieAI zombie;
    public GameObject Target;
    public GameObject Player;
    private int currentHealth;

    public float attackRadius = 5.0f;

    // Time it takes to complete a swing (in seconds)
    public float swingTime = 0.6f;
    // Damage per swing
    public int damagePerSwing = 25;
    // Flag to check if currently attacking
    private bool isAttacking = false;

    private float currentSwingTime = 0f;
    private float attackPeriod = 0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = zombie.zombieHealth;
        Debug.Log("starting zombie hp: " + currentHealth);
    }

    // Update is called once per frame
    void Update()
    {

        float distanceToTarget = Vector3.Distance(Player.transform.position, Target.transform.position);

        if (Input.GetButton("Fire1") && !isAttacking)
        {
            isAttacking = true;
            currentSwingTime = 0f;
            attackPeriod += Time.deltaTime;
            animator.SetBool("attacking", true);
            if (zombie != null && isAttacking && distanceToTarget <= attackRadius)
            {
                currentHealth -= damagePerSwing;
                Debug.Log("Zombie HP after atttack: " + currentHealth);
            }
            if(currentHealth <= 0)
            {
                Target.SetActive(false);
            }

        }
        /**
        else if (Input.GetButtonUp("Fire1"))
        {
            StopAttack();
        }
        **/

        if (isAttacking)
        {
            currentSwingTime += Time.deltaTime;

            if (currentSwingTime >= swingTime)
            {
                StopAttack();
            }
        }
    }


    void StopAttack()
    {
        isAttacking = false;
        currentSwingTime = 0f;
        animator.SetBool("attacking", false);
    }


}
