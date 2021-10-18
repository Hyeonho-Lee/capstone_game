using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Wolf_2 : MonoBehaviour
{
    public float spawn_delay;
    public float follow_delay;
    public bool is_follow;

    public GameObject attack_prefab_1;
    public GameObject attack_prefab_2;
    public GameObject attack_prefab_3;
    private GameObject follow_attack, player;

    void Start()
    {
        //Attack();
    }

    void Update()
    {
        if (is_follow && follow_attack != null) {
            follow_attack.transform.position = player.transform.position;
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

    [ContextMenu("Attack")]
    public void Attack()
    {
        player = GameObject.Find("Player").gameObject;

        StartCoroutine(Spawn_Attack(attack_prefab_1, spawn_delay * 0f));
        StartCoroutine(Spawn_Attack(attack_prefab_2, spawn_delay * 1f));
        StartCoroutine(Spawn_Attack(attack_prefab_3, spawn_delay * 2f));
    }
}
