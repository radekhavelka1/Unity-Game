using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("attacking", true);
                }
        else if(Input.GetButtonUp("Fire1"))
        { 
            animator.SetBool("attacking", false); 
        }

    }
}
