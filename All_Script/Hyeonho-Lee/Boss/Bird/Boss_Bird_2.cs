using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bird_2 : MonoBehaviour
{
    public float spawn_delay;
    public float count;

    public Vector3 range;
    private Vector3 random_range;

    public GameObject attack_prefab;
    private GameObject player;
    private GameObject spawn_attack;

    void Start()
    {
        //Attack();
    }

    IEnumerator Random_Spawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        random_range = new Vector3(Random.Range(player.transform.position.x - range.x, player.transform.position.x + range.x), 0f, Random.Range(player.transform.position.z - range.z, player.transform.position.z + range.z));
        spawn_attack = Instantiate(attack_prefab, this.transform);
        spawn_attack.transform.position = random_range;
    }

    [ContextMenu("Attack")]
    public void Attack()
    {
        player = GameObject.Find("Player").gameObject;
        for (int i = 0; i < count; i++) {
            StartCoroutine(Random_Spawn(spawn_delay * i));
        }
    }
}
