using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Wolf_1 : MonoBehaviour
{
    public float dash_speed;
    public float dash_wait_time;
    public int count_dash = 3;

    public float cooltime = 15f;
    public float real_time;
    public bool is_cool;

    private Vector3 dir;

    public GameObject attack_prefab;
    private GameObject target;
    private GameObject wolf;

    private Rigidbody rigidbody;
    private Wolf_FSM wolf_fsm;

    void Start()
    {
        wolf = GameObject.Find("Wolf_Patern");
        wolf_fsm = wolf.GetComponent<Wolf_FSM>();
    }

    private void Update()
    {
        if (dir != Vector3.zero && wolf_fsm.is_attack_1)
        {
            Wolf_Heading_Vec();
            Quaternion newRotation = Quaternion.LookRotation(dir * 10f * Time.deltaTime);
            wolf.transform.rotation = Quaternion.Slerp(wolf.transform.rotation, newRotation, 10f * Time.deltaTime);
        }

        if (!is_cool) {
            real_time += Time.deltaTime;

            if (real_time >= cooltime) {
                is_cool = true;
            }
        }
    }

    private Vector3 Wolf_Heading_Vec()
    {
        Vector3 heading = target.transform.position - transform.position;
        var distance = heading.magnitude;
        dir = heading / distance;

        return dir;
    }

    private void Add_Force(Vector3 dir, float speed)
    {
        rigidbody.AddForce(dir * speed, ForceMode.Impulse);
    }

    IEnumerator Dash(float delay)
    {
        Instantiate(attack_prefab, this.transform);
        yield return new WaitForSeconds(delay);
        Add_Force(Wolf_Heading_Vec(), dash_speed);
        yield return new WaitForSeconds(delay);
        Add_Force(Wolf_Heading_Vec(), dash_speed);
        yield return new WaitForSeconds(delay);
        Add_Force(Wolf_Heading_Vec(), dash_speed);
    }

    [ContextMenu("Attack")]
    public void Attack()
    {
        if (is_cool && wolf_fsm.is_attack_1) {
            target = GameObject.Find("Player");
            rigidbody = wolf.GetComponent<Rigidbody>();
            StartCoroutine(Dash(dash_wait_time));
            real_time = 0;
            is_cool = false;
        }
    }
}
