using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public int attack_count;

    public bool is_attack;
    private bool is_smash;

    private GameObject player;

    private PlayerMovement playermovement;
    private PlayerSound player_sound;
    private PlayerEffect player_effect;
    private CameraShake camera_shake;
    private Animator animator;
    private AudioSource audio;

    void Start()
    {
        player = GameObject.Find("Player");
        playermovement = player.GetComponent<PlayerMovement>();
        player_sound = player.GetComponent<PlayerSound>();
        player_effect = player.GetComponent<PlayerEffect>();
        animator = player.GetComponent<Animator>();
        audio = player.GetComponent<AudioSource>();
        camera_shake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
    }

    public void Attack_Start()
    {
        is_attack = true;
        playermovement.is_attack = true;
        playermovement.lock_dash = true;
        find_hit();
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

    void find_hit()
    {
        RaycastHit hit;
        float distance = 0f;

        if (attack_count == 1) {
            distance = 3.5f;
        }
        if (attack_count == 2) {
            distance = 4.5f;
        }
        if (attack_count == 3) {
            distance = 5.5f;
        }

        Debug.DrawRay(transform.position + new Vector3(0f, 2f, 0f), transform.forward * distance, Color.green, 0.5f);
        Debug.DrawRay(transform.position + new Vector3(2f, 2f, 0f), transform.forward * distance, Color.green, 0.5f);
        Debug.DrawRay(transform.position + new Vector3(-2f, 2f, 0f), transform.forward * distance, Color.green, 0.5f);

        if (Physics.Raycast(transform.position + new Vector3(0f, 2f, 0f), transform.forward * distance, out hit, distance) ||
            Physics.Raycast(transform.position + new Vector3(2f, 2f, 0f), transform.forward * distance, out hit, distance) ||
            Physics.Raycast(transform.position + new Vector3(-2f, 2f, 0f), transform.forward * distance, out hit, distance) ||
            Physics.Raycast(transform.position + new Vector3(1f, 2f, 0f), transform.forward * distance, out hit, distance) ||
            Physics.Raycast(transform.position + new Vector3(-1f, 2f, 0f), transform.forward * distance, out hit, distance)) {
            if (hit.transform.tag == "Boss_Patern_Wolf" || hit.transform.tag == "Enemy") {
                StartCoroutine(camera_shake.Shake(0.2f));
                if (attack_count == 1) {
                    GameObject effect_1 = Instantiate(player_effect.hit_effect_1);
                    effect_1.transform.position = hit.transform.position + new Vector3(0f, 2f, 0f);
                    Destroy(effect_1, 1.0f);
                    GameObject effect_1_1 = Instantiate(player_effect.hit_effect_2);
                    effect_1_1.transform.position = hit.transform.position + new Vector3(0f, 2f, 0f);
                    effect_1_1.transform.rotation = player.transform.rotation * Quaternion.Euler(0f, -45f, 90f);
                    Destroy(effect_1_1, 1.0f);
                }
                if (attack_count == 2) {
                    GameObject effect_2 = Instantiate(player_effect.hit_effect_1);
                    effect_2.transform.position = hit.transform.position + new Vector3(0f, 2f, 0f);
                    Destroy(effect_2, 1.0f);
                    GameObject effect_2_1 = Instantiate(player_effect.hit_effect_2);
                    effect_2_1.transform.position = hit.transform.position + new Vector3(0f, 2f, 0f);
                    effect_2_1.transform.rotation = player.transform.rotation * Quaternion.Euler(0f, 45f, 90f);
                    Destroy(effect_2_1, 1.0f);
                }
                if (attack_count == 3) {
                    GameObject effect_3 = Instantiate(player_effect.hit_effect_1);
                    effect_3.transform.position = hit.transform.position + new Vector3(0f, 2f, 0f);
                    Destroy(effect_3, 1.0f);
                    GameObject effect_3_1 = Instantiate(player_effect.hit_effect_2);
                    effect_3_1.transform.position = hit.transform.position + new Vector3(0f, 2f, 0f);
                    effect_3_1.transform.rotation = player.transform.rotation * Quaternion.Euler(0f, 0f, 90f);
                    Destroy(effect_3_1, 1.0f);
                }
            }
        }
    }
}
