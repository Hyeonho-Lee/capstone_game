using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    PlayerMovement playermovement;
    Animator animator;

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
        animator.SetBool("is_move", playermovement.is_move);
        animator.SetBool("is_dash", playermovement.is_dash);
        animator.SetBool("is_defence", playermovement.is_defence);
        animator.SetBool("is_attack", playermovement.is_attack);

        if(playermovement.is_attack == true) {
            animator.Play("attack");
        }
    }
}
