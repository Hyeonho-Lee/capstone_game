using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_0_Attack : MonoBehaviour
{
    private float move_speed = 5.0f;
    private float time = 0f;

    public GameObject attack_prefab;
    private GameObject attack_1;

    void Update()
    {
        time += Time.deltaTime;

        if (time >= 5f) {
            StartCoroutine(Attack());
            time = 0f;
        }

        if (attack_1 != null) {
            attack_1.transform.position += Vector3.right * move_speed * Time.deltaTime;
        }
    }

    IEnumerator Attack()
    {
        attack_1 = Instantiate(attack_prefab, this.transform);
        yield return null;
    }
}
