using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bird_3 : MonoBehaviour
{
    public float spawn_delay;
    public bool is_follow;

    public float real_time;
    public float cooltime;
    public bool is_cool;

    private float random_rotate;

    public GameObject attack_prefab;
    public GameObject skill_grid;
    private GameObject follow_attack, player;

    private GameObject bird;
    private GameObject skill_canvas;
    private GameObject attack_grid;
    private Bird_FSM bird_fsm;
    private Boss_Bird_Sound sound;

    void Start()
    {
        bird = GameObject.Find("Bird_Patern");
        skill_canvas = GameObject.Find("Indicators_Canvas");
        bird_fsm = bird.GetComponent<Bird_FSM>();
        sound = bird.GetComponent<Boss_Bird_Sound>();

        real_time = 40.0f;
        cooltime = 90.0f;
    }

    void Update()
    {
        if (is_follow && follow_attack != null) {
            follow_attack.transform.position = player.transform.position;
            attack_grid.transform.position = follow_attack.transform.position + new Vector3(0f, 0.5f, 0f);
        }

        if (!is_cool) {
            real_time += Time.deltaTime;
            if (real_time >= cooltime) {
                is_cool = true;
            }
        }
    }

    IEnumerator Follow_Attack(float delay)
    {
        is_follow = true;
        yield return new WaitForSeconds(delay);
        is_follow = false;
    }

    IEnumerator Spawn_Attack(float delay)
    {
        yield return new WaitForSeconds(delay);
        random_rotate = Random.Range(0f, 180f);
        follow_attack = Instantiate(attack_prefab);
        follow_attack.transform.rotation = Quaternion.Euler(0f, random_rotate, 0f);
        attack_grid = Instantiate(skill_grid, follow_attack.transform.position, follow_attack.transform.rotation);
        attack_grid.transform.SetParent(skill_canvas.transform);
        Destroy(attack_grid, 5f);
        StartCoroutine(Follow_Attack(spawn_delay));
    }

    IEnumerator Play_Sound(float delay, AudioClip audio)
    {
        yield return new WaitForSeconds(delay);
        sound.Sound_Play(audio);
    }

    [ContextMenu("Attack")]
    public void Attack()
    {
        player = GameObject.Find("Player").gameObject;
        if (is_cool && bird_fsm.is_attack_3) {
            for (int i = 0; i < 3; i++) {
                StartCoroutine(Spawn_Attack(10f * i));
                StartCoroutine(Play_Sound(10f * i + 5f, sound.skill_3_sound));
            }
            real_time = 0f;
            is_cool = false;
        }
    }
}
