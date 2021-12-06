using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Monkey_1 : MonoBehaviour
{
    public float chase_speed = 10.0f;

    public bool is_cool;

    public bool is_area;
    public bool is_sound;

    public GameObject attack_prefab;
    public GameObject skill_grid;
    private GameObject grid;

    private GameObject player;
    public GameObject attacks;
    private GameObject monkey;
    private GameObject monkey_render;
    private GameObject monkey_effect;

    private Vector3 dir;

    private SphereCollider collider;
    private Monkey_FSM monkey_fsm;
    private Boss_Monkey boss_monkey;
    private GameObject skill_canvas;
    private Boss_Monkey_Sound sound;
    private Boss_Monkey_Effect effect;

    void Start()
    {
        monkey = GameObject.Find("Monkey_Patern");
        monkey_render = GameObject.Find("Geometry");
        skill_canvas = GameObject.Find("Indicators_Canvas");
        boss_monkey = monkey.GetComponent<Boss_Monkey>();
        monkey_fsm = monkey.GetComponent<Monkey_FSM>();
        sound = monkey.GetComponent<Boss_Monkey_Sound>();
        effect = monkey.GetComponent<Boss_Monkey_Effect>();
    }

    void Update()
    {
        if(is_area)
        {
            Chase();
            monkey_fsm.is_attack_lock = true;
            StartCoroutine(Is_Sound(1.0f));
        }

        if(attacks != null) {
            monkey_render.SetActive(false);
            if (monkey_effect == null) {
                monkey_effect = Instantiate(effect.skill_1_effect, attacks.transform);
            }

            if (grid == null) {
                grid = Instantiate(skill_grid, this.transform.position, this.transform.rotation);
                grid.transform.SetParent(skill_canvas.transform);
            }
        } else {
            monkey_render.SetActive(true);
            if (monkey_effect != null) {
                Destroy(monkey_effect);
            }
            if (grid != null) {
                Destroy(grid);
            }
        }
    }


    void Chase()
    {
        dir = (player.transform.position - transform.position).normalized;
        monkey.transform.position += dir * chase_speed * Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player") 
        {
            StartCoroutine(Delay_Start(3f));
        }else if (other.tag == "Pillar") {
            Stun(other.transform.gameObject);
            collider.enabled = false;
        } else if (other.tag == "Pillar" && other.tag == "Player") {
            Stun(other.transform.gameObject);
            collider.enabled = false;
        }
    }

    void Stun(GameObject wall)
    {
        //Debug.Log("Ω∫≈œ");
        is_area = false;
        Destroy(wall);
        Destroy(attacks);
        monkey_fsm.is_attack_lock = false;
        StartCoroutine(monkey_fsm.attack_lock(5.5f));
        StartCoroutine(boss_monkey.Animation_Delay(0.0f, "monkey_stun"));
        sound.Sound_Play(sound.wall_sound);
    }

    IEnumerator Delay_Start(float delay)
    {
        if (is_area) {
            is_area = false;
            yield return new WaitForSeconds(delay);
            is_area = true;
        }
    }

    IEnumerator Destroy_delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        collider.enabled = false;
        is_area = false;
    }

    [ContextMenu("Attack")]
    public void Attack()
    {
        if (is_cool && monkey_fsm.is_attack_1) {
            is_area = true;
            player = GameObject.Find("Player").gameObject;
            collider = GetComponent<SphereCollider>();
            collider.enabled = true;
            attacks = Instantiate(attack_prefab, this.transform);
            StartCoroutine(Destroy_delay(30f));
            is_cool = false;
        }
    }

    IEnumerator Is_Sound(float delay)
    {
        if (!is_sound) {
            is_sound = true;
            sound.Sound_Play(sound.skill_1_sound);
            yield return new WaitForSeconds(delay);
            is_sound = false;
        }
    }
}
