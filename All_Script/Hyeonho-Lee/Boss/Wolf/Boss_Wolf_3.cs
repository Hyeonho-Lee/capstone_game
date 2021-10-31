using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Wolf_3 : MonoBehaviour
{
    public int amount_spawning_mobs;
    public float spawning_range;
    public float spawn_delay;

    public GameObject spawning_mobs;

    private GameObject player;
    private Vector3 player_position;

    void Start()
    {
        //Attack();
    }

    private Vector3 Get_Random_Position(float range)
    {
        float x = player.transform.position.x;
        float z = player.transform.position.z;

        float random_X = x + (Random.Range(-range, range));
        float random_z = z + (Random.Range(-range, range));

        Vector3 random_position = new Vector3(random_X, spawning_mobs.transform.localScale.y, random_z);

        return random_position;
    }

    IEnumerator Random_Spawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject spawn = Instantiate(spawning_mobs, Get_Random_Position(spawning_range), Quaternion.identity, this.transform);
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
        player = GameObject.Find("Player");
        Spawn_mob(amount_spawning_mobs, spawning_mobs);
    }
}
