using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (animator!=null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Attact", false);
                animator.SetBool("Idle", false);
            }
        }
    }
}
