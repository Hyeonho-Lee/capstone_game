using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bird_4 : MonoBehaviour
{
    public float move_speed;
    public float spawn_time;

    public float real_time;
    public float cooltime;
    public bool is_cool;

    public GameObject attack_prefab;
    private GameObject player;
    private GameObject attack_1;

    private GameObject bird;
    private Bird_FSM bird_fsm;
    private Boss_Bird_Sound sound;

    private Vector3 player_dir;

    void Start()
    {
        bird = GameObject.Find("Bird_Patern");
        sound = bird.GetComponent<Boss_Bird_Sound>();
        bird_fsm = bird.GetComponent<Bird_FSM>();
        cooltime = 20f;
        real_time = 10f;
        spawn_time = 2.0f;
    }

    void Update()
    {
        if (attack_1 != null) {
            attack_1.transform.position += player_dir * move_speed * Time.deltaTime;
        }

        if (!is_cool) {
            real_time += Time.deltaTime;
            if (real_time >= cooltime) {
                is_cool = true;
            }
        }
    }

    IEnumerator Is_Attack(float delay)
    {
        yield return new WaitForSeconds(delay);
        player_dir = (player.transform.position - this.transform.position).normalized;
        player_dir = new Vector3(player_dir.x, 0f, player_dir.z);
        attack_1 = Instantiate(attack_prefab);
        attack_1.transform.position = this.transform.position;
        sound.Sound_Play(sound.skill_4_sound);
    }

    [ContextMenu("Attack")]
    public void Attack()
    {
        if (is_cool && bird_fsm.is_attack_4) {
            player = GameObject.Find("Player").gameObject;

            StartCoroutine(Is_Attack(spawn_time));
            real_time = 0;
            is_cool = false;
        }
    }
}
