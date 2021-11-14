using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Wolf_3 : MonoBehaviour
{
    public int amount_spawning_mobs;
    public float spawning_range;
    public float spawn_delay;
    public float cooltime;
    public bool is_follow;

    public float real_time;
    public bool is_cool;

    public GameObject spawning_mobs;

    private GameObject player;
    private Vector3 player_position;
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
        Destroy(spawn, 10f);
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
