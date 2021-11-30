using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bird : MonoBehaviour
{
    public float bird_health;

    private bool is_damage;

    private Boss_UI_Controller boss_ui;
    private Animator animator;
    private Bird_FSM bird_fsm;
    private Boss_Bird_Sound sound;
    private PlayerData player_data;

    void Start()
    {
        boss_ui = GameObject.Find("System").GetComponent<Boss_UI_Controller>();
        player_data = GameObject.Find("System").GetComponent<PlayerData>();
        animator = GameObject.Find("bird woman").GetComponent<Animator>();
        bird_fsm = GetComponent<Bird_FSM>();
        sound = GetComponent<Boss_Bird_Sound>();

        Reset_Status();
    }

    void Update()
    {
        if (bird_health <= 0) {
            boss_ui.is_boss = false;
            player_data.playerDataTable.bird_boss = true;
            Destroy(this.gameObject);
        }

        Get_Value();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player_Attack") {
            StartCoroutine(Is_Damage(0.5f));
        }
    }

    void Reset_Status()
    {
        bird_health = 100.0f;
    }

    IEnumerator Is_Damage(float delay)
    {
        if (!is_damage) {
            is_damage = true;
            bird_health -= 1.0f;
            sound.Sound_Play(sound.hit_sound);
            yield return new WaitForSeconds(delay);
            is_damage = false;
        }
    }

    void Get_Value()
    {
        animator.SetBool("is_move", bird_fsm.is_move);
    }

    public IEnumerator Animation_Delay(float delay, string name)
    {
        yield return new WaitForSeconds(delay);
        animator.Play(name);
    }
}