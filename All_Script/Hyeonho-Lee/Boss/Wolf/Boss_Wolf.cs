using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Wolf : MonoBehaviour
{
    public float wolf_health;

    private bool is_damage;
    private bool is_die;

    public NPC_Dialogue dialogue;
    private Boss_UI_Controller boss_ui;
    private Animator animator;
    private Wolf_FSM wolf_fsm;
    private Boss_Wolf_Sound sound;
    private PlayerData player_data;
    private NPC_Manager manager;

    void Start()
    {
        boss_ui = GameObject.Find("System").GetComponent<Boss_UI_Controller>();
        player_data = GameObject.Find("System").GetComponent<PlayerData>();
        animator = GameObject.Find("wolf woman").GetComponent<Animator>();
        manager = GameObject.Find("System").GetComponent<NPC_Manager>();
        wolf_fsm = GetComponent<Wolf_FSM>();
        sound = GetComponent<Boss_Wolf_Sound>();
        Reset_Status();
    }

    void Update()
    {
        if (wolf_health <= 0.0f) 
        {
            boss_ui.is_boss = false;
            StartCoroutine(Die(3.0f, "dieing"));
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
        wolf_health = 150.0f;
    }

    IEnumerator Is_Damage(float delay, float damage)
    {
        if (!is_damage) 
        {
            is_damage = true;
            wolf_health -= damage;
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
        if (!is_die) {
            animator.Play(name);
        }
    }

    IEnumerator Die(float delay, string name)
    {
        if (!is_die) {
            is_die = true;

            dialogue.image_index = 2;
            dialogue.name = "화염의 정령   늑대";
            dialogue.sentences = new string[3];
            dialogue.sentences[0] = "앗.! 사실은.. 그냥 너랑 놀아준것 뿐이란 말이야!";
            dialogue.sentences[1] = "절-----대로 내가 바줬다는거 잊지마!";
            dialogue.sentences[2] = "알았으면 그냥 가란 말이야!!";

            manager.Start_Dialogue(dialogue);

            animator.Play(name);
            yield return new WaitForSeconds(delay);
            player_data.playerDataTable.wolf_boss = true;
            player_data.playerDataTable.is_stone_1 = true;
            Destroy(this.gameObject);
        }
    }
}
