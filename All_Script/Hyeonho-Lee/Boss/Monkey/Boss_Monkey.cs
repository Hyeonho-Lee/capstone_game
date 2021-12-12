using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Monkey : MonoBehaviour
{
    public float monkey_health;

    private bool is_damage;
    private bool health_0;
    private bool health_1;
    private bool health_2;
    private bool health_3;

    private bool is_die;

    public NPC_Dialogue dialogue;
    private Boss_UI_Controller boss_ui;
    private Animator animator;
    private Monkey_FSM monkey_fsm;
    private Boss_Monkey_1 pattern_1;
    private Boss_Monkey_3 pattern_3;
    private Boss_Monkey_Sound sound;
    private PlayerData player_data;
    private NPC_Manager manager;

    void Start()
    {
        boss_ui = GameObject.Find("System").GetComponent<Boss_UI_Controller>();
        player_data = GameObject.Find("System").GetComponent<PlayerData>();
        animator = GameObject.Find("monkey man").GetComponent<Animator>();
        pattern_1 = GameObject.Find("Monkey_Patern_1").GetComponent<Boss_Monkey_1>();
        pattern_3 = GameObject.Find("Monkey_Patern_3").GetComponent<Boss_Monkey_3>();
        manager = GameObject.Find("System").GetComponent<NPC_Manager>();
        monkey_fsm = GetComponent<Monkey_FSM>();
        sound = GetComponent<Boss_Monkey_Sound>();
        Reset_Status();
    }

    void Update()
    {
        if (monkey_health <= 0.0f) 
        {
            boss_ui.is_boss = false;
            StartCoroutine(Die(3.0f, "dieing"));
        }

        if (monkey_health <= 150.0f / 5.0f * 4.0f && !health_0) {
            health_0 = true;
            pattern_1.is_cool = true;
        }

        if (monkey_health <= 150.0f / 5.0f * 3.0f && !health_1) {
            health_1 = true;
            pattern_1.is_cool = true;
        }

        if (monkey_health <= 150.0f / 5.0f * 2.0f && !health_2) {
            health_2 = true;
            pattern_1.is_cool = true;
        }

        if (monkey_health <= 150.0f / 5.0f * 1.0f && !health_3) {
            health_3 = true;
            pattern_3.is_cool = true;
            pattern_3.is_puzzle = true;
        }

        Get_Value();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player_Attack") {
            StartCoroutine(Is_Damage(0.5f, 1.0f));
        }

        if (other.tag == "Player_Attack_3") {
            StartCoroutine(Is_Damage(0.5f, 2.0f));
        }

        if (other.tag == "Player_Attack_4") {
            StartCoroutine(Is_Damage(0.5f, 2.5f));
        }
    }

    void Reset_Status()
    {
        monkey_health = 150.0f;
    }

    IEnumerator Is_Damage(float delay, float damage)
    {
        if (!is_damage && !pattern_3.is_puzzle) {
            is_damage = true;
            monkey_health -= damage;
            sound.Sound_Play(sound.hit_sound);
            yield return new WaitForSeconds(delay);
            is_damage = false;
        }
    }

    void Get_Value()
    {
        animator.SetBool("is_move", monkey_fsm.is_move);
    }

    public IEnumerator Animation_Delay(float delay, string name)
    {
        yield return new WaitForSeconds(delay);
        if (!is_die) {
            animator.Play(name);
        }
    }

    IEnumerator Die(float delay, string name)
    {
        if (!is_die) {
            is_die = true;

            dialogue.image_index = 4;
            dialogue.name = "바위의 정령   원숭이";
            dialogue.sentences = new string[3];
            dialogue.sentences[0] = "으으.. 내가 무슨짓을 저지른 것이지...";
            dialogue.sentences[1] = "미안하다 모모타로 내가 정신이 나갔었던것 같구나";
            dialogue.sentences[2] = "다시 나의 힘을 받아다오";

            manager.Start_Dialogue(dialogue);

            animator.Play(name);
            yield return new WaitForSeconds(delay);
            player_data.playerDataTable.monkey_boss = true;
            player_data.playerDataTable.is_stone_3 = true;
            Destroy(this.gameObject);
        }
    }
}
