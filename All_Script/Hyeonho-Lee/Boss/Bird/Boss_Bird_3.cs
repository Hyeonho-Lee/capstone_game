using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bird_3 : MonoBehaviour
{
    public float spawn_delay;
    public bool is_follow;

    private float random_rotate;

    public GameObject attack_prefab;
    private GameObject follow_attack, player;

    void Start()
    {
        //Attack();
    }

    void Update()
    {
        if(is_follow && follow_attack != null) {
            follow_attack.transform.position = player.transform.position;
        }
    }

    IEnumerator Follow_Attack(float delay)
    {
        is_follow = true;
        yield return new WaitForSeconds(delay);
        is_follow = false;
    }


    IEnumerator Spawn_Attack(float delay)
    {
        yield return new WaitForSeconds(delay);
        random_rotate = Random.Range(0f, 180f);
        follow_attack = Instantiate(attack_prefab);
        follow_attack.transform.rotation = Quaternion.Euler(0f, random_rotate, 0f);
        StartCoroutine(Follow_Attack(spawn_delay));
    }

    [ContextMenu("Attack")]
    public void Attack()
    {
        player = GameObject.Find("Player").gameObject;

        for (int i = 0; i < 5; i++) {
            StartCoroutine(Spawn_Attack(10f * i));
        }
    }
}
