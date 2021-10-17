using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Wolf_Pattern3 : MonoBehaviour
{
    public int amount_spawning_mobs;
    public float spawning_range;

    public GameObject spawning_mobs;

    private GameObject player;
    private Vector3 player_position;
  
    void Start()
    {
        player = GameObject.Find("Player");
        Spawn_mob(amount_spawning_mobs, spawning_mobs);
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
    
    void Spawn_mob(int amount,GameObject mob)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(mob,Get_Random_Position(spawning_range),Quaternion.identity);
        }
    }
}
