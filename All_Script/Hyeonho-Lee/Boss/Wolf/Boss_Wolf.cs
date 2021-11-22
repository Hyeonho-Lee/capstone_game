using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Wolf : MonoBehaviour
{
    public float wolf_health;

    private bool is_damage;

    private Boss_UI_Controller boss_ui;
    private Animator animator;
    private Wolf_FSM wolf_fsm;
    private Boss_Wolf_Sound sound;
    private PlayerData player_data;

    void Start()
    {
        boss_ui = GameObject.Find("System").GetComponent<Boss_UI_Controller>();
        player_data = GameObject.Find("System").GetComponent<PlayerData>();
        animator = GameObject.Find("wolf woman").GetComponent<Animator>();
        wolf_fsm = GetComponent<Wolf_FSM>();
        sound = GetComponent<Boss_Wolf_Sound>();
        Reset_Status();
    }

    void Update()
    {
        if (wolf_health <= 0) 
        {
            boss_ui.is_boss = false;
            player_data.playerDataTable.wolf_boss = true;
            Destroy(this.gameObject);
        }

        Get_Value();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player_Attack") 
        {
            StartCoroutine(Is_Damage(0.5f));
        }
    }

    void Reset_Status()
    {
        wolf_health = 150.0f;
    }

    IEnumerator Is_Damage(float delay)
    {
        if (!is_damage) 
        {
            is_damage = true;
            wolf_health -= 1.0f;
            sound.Hit_Sound_Play();
            yield return new WaitForSeconds(delay);
            is_damage = false;
        }
    }

    void Get_Value()
    {
        animator.SetBool("is_move", wolf_fsm.is_move);
    }

    public IEnumerator Animation_Delay(float delay, string name)
    {
        yield return new WaitForSeconds(delay);
        animator.Play(name);
    }
}
