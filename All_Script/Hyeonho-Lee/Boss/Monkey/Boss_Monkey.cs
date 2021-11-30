using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Monkey : MonoBehaviour
{
    public float monkey_health;

    private bool is_damage;

    private Boss_UI_Controller boss_ui;
    private Animator animator;

    void Start()
    {
        boss_ui = GameObject.Find("System").GetComponent<Boss_UI_Controller>();
        Reset_Status();
    }

    void Update()
    {
        if (monkey_health <= 0) {
            boss_ui.is_boss = false;
            //player_data.playerDataTable.wolf_boss = true;
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
        monkey_health = 100.0f;
    }

    IEnumerator Is_Damage(float delay)
    {
        if (!is_damage) {
            is_damage = true;
            monkey_health -= 1.0f;
            //sound.Hit_Sound_Play();
            yield return new WaitForSeconds(delay);
            is_damage = false;
        }
    }

    void Get_Value()
    {
        //animator.SetBool("is_move", wolf_fsm.is_move);
    }

    public IEnumerator Animation_Delay(float delay, string name)
    {
        yield return new WaitForSeconds(delay);
        animator.Play(name);
    }
}
