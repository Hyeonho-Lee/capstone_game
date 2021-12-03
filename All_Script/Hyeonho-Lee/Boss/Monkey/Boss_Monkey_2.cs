using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Monkey_2 : MonoBehaviour
{
    public float interval_time;

    public float cooltime;
    public float real_time;
    public bool is_cool;

    public GameObject attack_1;
    public GameObject attack_2;
    public GameObject attack_3;

    private GameObject monkey;

    private Monkey_FSM monkey_fsm;
    private GameObject skill_canvas;
    private Boss_Monkey_Sound sound;
    private Boss_Monkey_Effect effect;

    void Start()
    {
        monkey = GameObject.Find("Monkey_Patern");
        skill_canvas = GameObject.Find("Indicators_Canvas");
        monkey_fsm = monkey.GetComponent<Monkey_FSM>();
        sound = monkey.GetComponent<Boss_Monkey_Sound>();
        effect = monkey.GetComponent<Boss_Monkey_Effect>();
        cooltime = 35.0f;
        real_time = 1.0f;
    }

    void Update()
    {
        if (!is_cool) {
            real_time += Time.deltaTime;
            if (real_time >= cooltime) {
                is_cool = true;
            }
        }
    }

    IEnumerator Attack_Spawn(GameObject attack_object, float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(attack_object, this.transform);
    }

    [ContextMenu("Attack")]
    public void Attack()
    {
        if (is_cool && monkey_fsm.is_attack_2) {
            StartCoroutine(Attack_Spawn(attack_1, 0f));
            StartCoroutine(Attack_Spawn(attack_2, interval_time));
            StartCoroutine(Attack_Spawn(attack_3, interval_time * 2f));
            real_time = 0;
            is_cool = false;
        }
    }
}
