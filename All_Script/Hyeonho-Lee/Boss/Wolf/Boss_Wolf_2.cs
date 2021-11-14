using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Wolf_2 : MonoBehaviour
{
    public float spawn_delay;
    public float follow_delay;
    public float cooltime;
    public bool is_follow;

    public float real_time;
    public bool is_cool;

    public GameObject attack_prefab_1;
    public GameObject attack_prefab_2;
    public GameObject attack_prefab_3;
    private GameObject follow_attack, player;
    private GameObject wolf;
    private Wolf_FSM wolf_fsm;

    void Start()
    {
        //Attack();
        wolf = GameObject.Find("Wolf_Patern");
        wolf_fsm = wolf.GetComponent<Wolf_FSM>();
    }

    void Update()
    {
        if (is_follow && follow_attack != null) {
            follow_attack.transform.position = player.transform.position;
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


    IEnumerator Spawn_Attack(GameObject attacks, float delay)
    {
        yield return new WaitForSeconds(delay);
        follow_attack = Instantiate(attacks);
        Destroy(follow_attack, 7.3f);
        StartCoroutine(Follow_Attack(follow_delay));
    }

    IEnumerator Teleport_Boss(float delay)
    {
        yield return new WaitForSeconds(delay);
        wolf.transform.position = follow_attack.transform.position + new Vector3(0f, 1.5f, 0f);
    }

    [ContextMenu("Attack")]
    public void Attack()
    {
        if (is_cool && wolf_fsm.is_attack_2) {
            player = GameObject.Find("Player").gameObject;

            StartCoroutine(Spawn_Attack(attack_prefab_1, spawn_delay * 0f));
            StartCoroutine(Spawn_Attack(attack_prefab_2, spawn_delay * 1f));
            StartCoroutine(Spawn_Attack(attack_prefab_3, spawn_delay * 2f));
            StartCoroutine(Teleport_Boss(spawn_delay * 2f + 3f));

            real_time = 0;
            is_cool = false;
        }
    }
}
