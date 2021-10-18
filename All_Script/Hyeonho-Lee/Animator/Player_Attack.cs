using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    private PlayerMovement playermovement;
    private Animator animator;

    void Start()
    {
        playermovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        animator = GameObject.Find("Player").GetComponent<Animator>();
    }

    public void Combo_True()
    {
        playermovement.attack_start = true;
    }

    public void Combo() 
    {
        if (playermovement.attack_combo == 2) 
        {
            animator.Play("attack_2");
        }

        if (playermovement.attack_combo == 3) {
            animator.Play("attack_3");
        }
    }

    public void Combo_Reset()
    {
        playermovement.attack_start = false;
        playermovement.attack_combo = 0;
    }
}
