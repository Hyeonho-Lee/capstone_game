using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Monkey_2 : MonoBehaviour
{
    public float interval_time;

    public GameObject attack_1;
    public GameObject attack_2;
    public GameObject attack_3;

    void Start()
    {
        //Attack();
    }

    IEnumerator Attack_Spawn(GameObject attack_object, float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(attack_object, this.transform);
    }

    [ContextMenu("Attack")]
    public void Attack()
    {
        StartCoroutine(Attack_Spawn(attack_1, 0f));
        StartCoroutine(Attack_Spawn(attack_2, interval_time));
        StartCoroutine(Attack_Spawn(attack_3, interval_time * 2f));
    }
}
