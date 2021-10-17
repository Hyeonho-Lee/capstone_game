using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Monkey_1 : MonoBehaviour
{
    public float chase_speed = 10.0f;

    private bool is_area;

    public GameObject attack_prefab;
    private GameObject player;
    private GameObject attacks;

    private Vector3 dir;

    private SphereCollider collider;

    void Start()
    {
        //Attack();
    }

    void Update()
    {
        if(is_area)
        {
            Chase();
        }
    }


    void Chase()
    {
        dir = (player.transform.position - transform.position).normalized;
        this.transform.position += dir * chase_speed * Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player") 
        {
            is_area = false;
            if (!is_area) 
            {
                StartCoroutine(Delay_Start(3f));
            }
        }

        if (other.tag == "Pillar") {
            Debug.Log("error");
        }

        Debug.Log(other.name);
    }

    void Stun()
    {
        Debug.Log("¹¹Áö");
    }

    IEnumerator Delay_Start(float delay)
    {
        yield return new WaitForSeconds(delay);
        is_area = true;
    }

    IEnumerator Destroy_delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        collider.enabled = false;
        is_area = false;
    }

    [ContextMenu("Attack")]
    public void Attack()
    {
        is_area = true;
        player = GameObject.Find("Player").gameObject;
        collider = GetComponent<SphereCollider>();
        collider.enabled = true;
        attacks = Instantiate(attack_prefab, this.transform);
        StartCoroutine(Destroy_delay(30f));
    }
}
