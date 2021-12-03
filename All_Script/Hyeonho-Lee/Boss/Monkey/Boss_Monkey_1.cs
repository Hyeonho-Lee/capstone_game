using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Monkey_1 : MonoBehaviour
{
    public float chase_speed = 10.0f;

    public bool is_cool;

    public bool is_area;

    public GameObject attack_prefab;
    private GameObject player;
    public GameObject attacks;
    private GameObject monkey;

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
        }

        if (other.tag == "Pillar") {
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
}
