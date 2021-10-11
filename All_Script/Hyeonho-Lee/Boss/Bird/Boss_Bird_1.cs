using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bird_1 : MonoBehaviour
{
    public float move_speed;
    public float move_time;
    public float wait_time;

    public bool is_attack_1;
    public bool is_wait;

    public GameObject attack_prefab, wait_prefab;
    private GameObject player, attack_1, attack_2, attack_3;

    private Vector3 forward;
    private Vector3 right_45;
    private Vector3 left_45;

    void Start()
    {
        //Attack();
    }

    void Update()
    {
        if (is_attack_1) 
        {
            attack_1.transform.position += forward * move_speed * Time.deltaTime;
            attack_2.transform.position += right_45 * move_speed * Time.deltaTime;
            attack_3.transform.position += left_45 * move_speed * Time.deltaTime;
        }
    }

    IEnumerator Is_Attack(float delay)
    {
        is_attack_1 = true;

        player = GameObject.Find("Player").gameObject;
        forward = (player.transform.position - this.transform.position).normalized;
        right_45 = Quaternion.Euler(0f, 45f, 0f) * forward;
        left_45 = Quaternion.Euler(0f, 315f, 0f) * forward;

        attack_1 = Instantiate(attack_prefab, this.transform);
        attack_2 = Instantiate(attack_prefab, this.transform);
        attack_3 = Instantiate(attack_prefab, this.transform);
        yield return new WaitForSeconds(delay);
        is_attack_1 = false;
    }
    IEnumerator Is_Wait(float delay)
    {
        Instantiate(wait_prefab, this.transform);
        yield return new WaitForSeconds(delay);
        StartCoroutine(Is_Attack(move_time));
    }

    [ContextMenu("Attack")]
    public void Attack()
    {
        StartCoroutine(Is_Wait(wait_time));
    }
}
