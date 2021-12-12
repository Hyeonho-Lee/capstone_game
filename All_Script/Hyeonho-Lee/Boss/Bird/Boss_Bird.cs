using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bird : MonoBehaviour
{
    public float bird_health;

    private bool is_damage;
    private bool is_die;

    public NPC_Dialogue dialogue;
    private Boss_UI_Controller boss_ui;
    private Animator animator;
    private Bird_FSM bird_fsm;
    private Boss_Bird_Sound sound;
    private PlayerData player_data;
    private NPC_Manager manager;

    void Start()
    {
        boss_ui = GameObject.Find("System").GetComponent<Boss_UI_Controller>();
        player_data = GameObject.Find("System").GetComponent<PlayerData>();
        animator = GameObject.Find("bird woman").GetComponent<Animator>();
        manager = GameObject.Find("System").GetComponent<NPC_Manager>();
        bird_fsm = GetComponent<Bird_FSM>();
        sound = GetComponent<Boss_Bird_Sound>();

        Reset_Status();
    }

    void Update()
    {
        if (bird_health <= 0.0f) {
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
        bird_health = 150.0f;
    }

    IEnumerator Is_Damage(float delay, float damage)
    {
        if (!is_damage) {
            is_damage = true;
            bird_health -= damage;
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
        if (!is_die) {
            animator.Play(name);
        }
    }

    IEnumerator Die(float delay, string name)
    {
        if (!is_die) {
            is_die = true;

            dialogue.image_index = 3;
            dialogue.name = "번개의 정령   학";
            dialogue.sentences = new string[3];
            dialogue.sentences[0] = "음.. 모모타로 다시 보니 반갑군...";
            dialogue.sentences[1] = "내가 무슨 이유로 여기서 너와 싸웠는지는 모르겟지만";
            dialogue.sentences[2] = "뭐... 내가 너의 힘이 되어주마";

            manager.Start_Dialogue(dialogue);

            animator.Play(name);
            yield return new WaitForSeconds(delay);
            player_data.playerDataTable.bird_boss = true;
            player_data.playerDataTable.is_stone_2 = true;
            Destroy(this.gameObject);
        }
    }
}