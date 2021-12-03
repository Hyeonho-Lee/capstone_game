using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Monkey_3 : MonoBehaviour
{
    public bool is_cool;

    public bool is_puzzle;

    public GameObject attack_prefab;
    private GameObject player;
    private GameObject attacks;
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
    }

    void Update()
    {
        
    }

    IEnumerator Attack_Spawn(GameObject attack_object, float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(attack_object, this.transform);
    }

    [ContextMenu("Attack")]
    public void Attack()
    {
        if (is_cool && monkey_fsm.is_attack_3) {
            player = GameObject.Find("Player").gameObject;
            StartCoroutine(Attack_Spawn(attack_prefab, 0f));
            monkey_fsm.is_attack_lock = true;
            monkey.transform.position = new Vector3(-150f, 0.5f, 550f);
            is_cool = false;
        }
    }
}
