using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bird_4 : MonoBehaviour
{
    public float move_speed;
    public float spawn_time;

    public GameObject attack_prefab;
    private GameObject player;
    private GameObject attack_1;

    private Vector3 player_dir;

    void Start()
    {
        //Attack();
    }

    void Update()
    {
        if(attack_1 != null) {
            attack_1.transform.position += player_dir * move_speed * Time.deltaTime;
        }
    }

    IEnumerator Is_Attack(float delay)
    {
        yield return new WaitForSeconds(delay);
        player_dir = (player.transform.position - this.transform.position).normalized;
        attack_1 = Instantiate(attack_prefab, this.transform);
        attack_1.transform.position = this.transform.position;
    }

    [ContextMenu("Attack")]
    public void Attack()
    {
        player = GameObject.Find("Player").gameObject;

        StartCoroutine(Is_Attack(spawn_time));
    }
}
