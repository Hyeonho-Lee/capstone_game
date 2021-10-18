using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private PlayerMovement playermovement;
    private Animator animator;

    void Start()
    {
        playermovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Get_Value();
    }

    void Get_Value()
    {
        animator.SetFloat("horizontal", playermovement.h_axis);
        animator.SetFloat("vertical", playermovement.v_axis);
        animator.SetFloat("attack_combo", playermovement.attack_combo);
        animator.SetBool("is_move", playermovement.is_move);
        animator.SetBool("is_dash", playermovement.is_dash);
        animator.SetBool("is_defence", playermovement.is_defence);
        animator.SetBool("is_attack", playermovement.is_attack);

        if (playermovement.is_attack == true) 
        {
            animator.Play("attack_1");
        }
    }
}
