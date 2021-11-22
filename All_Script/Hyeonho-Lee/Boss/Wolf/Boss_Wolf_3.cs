using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Wolf_3 : MonoBehaviour
{
    public int amount_spawning_mobs;
    public float spawning_range;
    public float spawn_delay;
    public bool is_follow;

    public float cooltime;
    public float real_time;
    public bool is_cool;

    public GameObject spawning_mobs;
    public GameObject skill_grid;

    private GameObject player;
    private Vector3 player_position;
    private GameObject wolf;
    private GameObject skill_canvas;
    private Wolf_FSM wolf_fsm;
    private Boss_Wolf_Sound sound;
    private Boss_Wolf_Effect effect;

    void Start()
    {
        wolf = GameObject.Find("Wolf_Patern");
        wolf_fsm = wolf.GetComponent<Wolf_FSM>();
        sound = wolf.GetComponent<Boss_Wolf_Sound>();
        effect = wolf.GetComponent<Boss_Wolf_Effect>();
        skill_canvas = GameObject.Find("Indicators_Canvas");
        cooltime = 75.0f;
        real_time = 10.0f;
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

    private Vector3 Get_Random_Position(float range)
    {
        float x = player.transform.position.x;
        float z = player.transform.position.z;

        float random_X = x + (Random.Range(-range, range));
        float random_z = z + (Random.Range(-range, range));

        Vector3 random_position = new Vector3(random_X, 0f, random_z);

        return random_position;
    }

    IEnumerator Random_Spawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject spawn = Instantiate(spawning_mobs, Get_Random_Position(spawning_range), Quaternion.identity);
        Destroy(spawn, 5f);
        GameObject grid = Instantiate(skill_grid, spawn.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        grid.transform.SetParent(skill_canvas.transform);
        Destroy(grid, 5f);
        yield return new WaitForSeconds(3.0f);
        sound.SKill_3_Sound_Play();
        GameObject effects = Instantiate(effect.skill_3_effect, spawn.transform.position, Quaternion.identity);
        Destroy(effects, 3f);
    }


    void Spawn_mob(int amount, GameObject mob)
    {
        for (int i = 0; i < amount; i++) 
        {
            StartCoroutine(Random_Spawn(spawn_delay * i));
        }
    }

    [ContextMenu("Attack")]
    public void Attack()
    {
        if (is_cool && wolf_fsm.is_attack_3) {
            player = GameObject.Find("Player");
            Spawn_mob(amount_spawning_mobs, spawning_mobs);
            real_time = 0;
            is_cool = false;
        }
    }
}
