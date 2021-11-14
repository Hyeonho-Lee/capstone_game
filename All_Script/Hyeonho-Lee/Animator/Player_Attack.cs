using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public int attack_count;

    public bool is_attack;
    private bool is_smash;

    public AudioClip attack_1_sound;
    private GameObject player;

    private PlayerMovement playermovement;
    private Animator animator;
    private AudioSource audio;

    void Start()
    {
        player = GameObject.Find("Player");
        playermovement = player.GetComponent<PlayerMovement>();
        animator = player.GetComponent<Animator>();
        audio = player.GetComponent<AudioSource>();
    }

    public void Attack_Start()
    {
        is_attack = true;
        playermovement.is_attack = true;
        playermovement.lock_dash = true;
    }

    public void Attack_Next()
    {
        if (!is_smash) {
            if (attack_count == 2) {
                animator.Play("attack_2");
                playermovement.attack_object_1.gameObject.SetActive(false);
                playermovement.attack_object_2.gameObject.SetActive(true);
            }
            if (attack_count == 3) {
                animator.Play("attack_3");
                playermovement.attack_object_2.gameObject.SetActive(false);
                playermovement.attack_object_3.gameObject.SetActive(true);
            }

            if (attack_count >= 4) {
                attack_count = 0;
                return;
            }
        }
    }

    public void Attack_Reset()
    {
        is_attack = false;
        playermovement.is_attack = false;
        playermovement.lock_dash = false;
        playermovement.attack_object_1.gameObject.SetActive(false);
        playermovement.attack_object_2.gameObject.SetActive(false);
        playermovement.attack_object_3.gameObject.SetActive(false);
        is_smash = false;
        attack_count = 0;
    }

    public void Attack()
    {
        if (attack_count == 0) 
        {
            animator.Play("attack_1");
            playermovement.attack_object_1.gameObject.SetActive(true);
            attack_count = 1;
            return;
        }

        if (attack_count != 0) 
        {
            if (is_attack) 
            {
                is_attack = false;
                playermovement.is_attack = false;
                playermovement.lock_dash = false;
                attack_count += 1;
            }
        }
    }

    public void Smash()
    {
        if (is_attack) 
        {
            is_attack = false;
            playermovement.is_attack = false;
            playermovement.lock_dash = false;
            is_smash = true;
        }
    }

    public void Attack_Sound(AudioClip clip)
    {
        audio.PlayOneShot(clip);
    }
}
