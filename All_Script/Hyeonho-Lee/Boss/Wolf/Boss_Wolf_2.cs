using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Wolf_2 : MonoBehaviour
{
    public float spawn_delay;
    public float follow_delay;
    public bool is_follow;

    public float cooltime;
    public float real_time;
    public bool is_cool;

    public GameObject attack_prefab_1;
    public GameObject attack_prefab_2;
    public GameObject attack_prefab_3;
    public GameObject skill_grid_1;
    public GameObject skill_grid_2;
    public GameObject skill_grid_3;
    private GameObject skill_canvas;
    private GameObject follow_attack, player;
    private GameObject wolf;
    private GameObject attack_grid;
    private Wolf_FSM wolf_fsm;
    private Boss_Wolf_Sound sound;
    private Boss_Wolf_Effect effect;

    void Start()
    {
        wolf = GameObject.Find("Wolf_Patern");
        skill_canvas = GameObject.Find("Indicators_Canvas");
        wolf_fsm = wolf.GetComponent<Wolf_FSM>();
        sound = wolf.GetComponent<Boss_Wolf_Sound>();
        effect = wolf.GetComponent<Boss_Wolf_Effect>();
        cooltime = 60f;
        real_time = 10f;
    }

    void Update()
    {
        if (is_follow && follow_attack != null && attack_grid != null) {
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


    IEnumerator Spawn_Attack(GameObject attacks, float delay)
    {
        yield return new WaitForSeconds(delay);
        follow_attack = Instantiate(attacks);
        Destroy(follow_attack, 7.3f);
        if (attacks.name == "BossAttacl_Box_2") {
            attack_grid = Instantiate(skill_grid_1, follow_attack.transform.position, Quaternion.identity);
            attack_grid.transform.SetParent(skill_canvas.transform);
            Destroy(attack_grid, 5f);
        }
        if (attacks.name == "BossAttacl_Box_3") {
            attack_grid = Instantiate(skill_grid_2, follow_attack.transform.position, Quaternion.identity * Quaternion.Euler(0f, 45f, 0f));
            attack_grid.transform.SetParent(skill_canvas.transform);
            Destroy(attack_grid, 5f);
        }
        if (attacks.name == "BossAttack_Sphere_4") {
            attack_grid = Instantiate(skill_grid_3, follow_attack.transform.position, Quaternion.identity);
            attack_grid.transform.SetParent(skill_canvas.transform);
            Destroy(attack_grid, 3f);
        }
        //GameObject grid = Instantiate(skill_grid_1, follow_attack.transform.position, Quaternion.identity);
        //Destroy(grid, 3.0f);
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
            StartCoroutine(Sound_Delay_1(spawn_delay * 0f + 3f));
            StartCoroutine(Spawn_Attack(attack_prefab_2, spawn_delay * 1f));
            StartCoroutine(Sound_Delay_2(spawn_delay * 1f + 3f));
            StartCoroutine(Spawn_Attack(attack_prefab_3, spawn_delay * 2f));
            StartCoroutine(Sound_Delay_3(spawn_delay * 2f + 3f));
            StartCoroutine(Teleport_Boss(spawn_delay * 2f + 3f));

            real_time = 0;
            is_cool = false;
        }
    }

    IEnumerator Sound_Delay_1(float delay)
    {
        yield return new WaitForSeconds(delay);
        sound.SKill_2_1_Sound_Play();
        GameObject skill_2_1 = Instantiate(effect.skill_2_1_effect, follow_attack.transform.position + new Vector3(0f, 0f, -5f), Quaternion.identity);
        Destroy(skill_2_1, 3.0f);
    }

    IEnumerator Sound_Delay_2(float delay)
    {
        yield return new WaitForSeconds(delay);
        sound.SKill_2_2_Sound_Play();
        GameObject skill_2_2 = Instantiate(effect.skill_2_2_effect, follow_attack.transform.position + new Vector3(-4.5f, 0f, -1.5f), Quaternion.Euler(0f, 45f, 0f));
        Destroy(skill_2_2, 3.0f);
    }

    IEnumerator Sound_Delay_3(float delay)
    {
        yield return new WaitForSeconds(delay);
        sound.SKill_2_3_Sound_Play();
        GameObject skill_2_3 = Instantiate(effect.skill_2_3_effect, follow_attack.transform.position, Quaternion.identity);
        Destroy(skill_2_3, 2.0f);
    }
}
