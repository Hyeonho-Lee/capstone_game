using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Wolf_1 : MonoBehaviour
{
    public float dash_speed;
    public float dash_wait_time;
    public int count_dash = 3;

    public float cooltime;
    public float real_time;
    public bool is_cool;

    private Vector3 dir;

    public GameObject attack_prefab;
    public GameObject skill_grid;
    private GameObject target;
    private GameObject wolf;
    private GameObject skill_canvas;
    private GameObject skill_grids;

    private Rigidbody rigidbody;
    private Wolf_FSM wolf_fsm;
    private Boss_Wolf_Sound sound;
    private Boss_Wolf_Effect effect;

    void Start()
    {
        wolf = GameObject.Find("Wolf_Patern");
        skill_canvas = GameObject.Find("Indicators_Canvas");
        wolf_fsm = wolf.GetComponent<Wolf_FSM>();
        sound = wolf.GetComponent<Boss_Wolf_Sound>();
        effect = wolf.GetComponent<Boss_Wolf_Effect>();
        cooltime = 45f;
        real_time = 10f;
    }

    private void Update()
    {
        if (dir != Vector3.zero)
        {
            Wolf_Heading_Vec();
            Quaternion newRotation = Quaternion.LookRotation(dir * 10f * Time.deltaTime);
            Quaternion newRotations = Quaternion.Euler(0f, newRotation.eulerAngles.y, 0f);
            wolf.transform.rotation = Quaternion.Slerp(wolf.transform.rotation, newRotation, 10f * Time.deltaTime);
        }

        if (!is_cool) {
            real_time += Time.deltaTime;

            if (real_time >= cooltime) {
                is_cool = true;
            }
        }

        if (skill_grids != null) {
            Wolf_Heading_Vec();
            Quaternion newRotation = Quaternion.LookRotation(dir * 10f * Time.deltaTime);
            Quaternion newRotations = Quaternion.Euler(0f, newRotation.eulerAngles.y, 0f);
            skill_grids.transform.rotation = Quaternion.Slerp(wolf.transform.rotation, newRotations, 10f * Time.deltaTime);
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
        skill_grids = Instantiate(skill_grid, transform.position + new Vector3(0f, 0.5f, 0f), wolf.transform.rotation);
        skill_grids.transform.SetParent(skill_canvas.transform);
        Destroy(skill_grids, 2f);

        yield return new WaitForSeconds(delay);
        Add_Force(Wolf_Heading_Vec(), dash_speed);
        sound.SKill_1_1_Sound_Play();
        GameObject effects = Instantiate(effect.skill_1_2_effect, transform.position + new Vector3(0f, 3f, 2f), Quaternion.identity);
        Destroy(effects, 2f);

        skill_grids = Instantiate(skill_grid, transform.position + new Vector3(0f, 0.5f, 0f), wolf.transform.rotation);
        skill_grids.transform.SetParent(skill_canvas.transform);
        Destroy(skill_grids, 2f);
        yield return new WaitForSeconds(delay);
        Add_Force(Wolf_Heading_Vec(), dash_speed);
        sound.SKill_1_2_Sound_Play();
        GameObject effectss = Instantiate(effect.skill_1_2_effect, transform.position + new Vector3(0f, 3f, 2f), Quaternion.identity);
        Destroy(effectss, 2f);

        skill_grids = Instantiate(skill_grid, transform.position + new Vector3(0f, 0.5f, 0f), wolf.transform.rotation);
        skill_grids.transform.SetParent(skill_canvas.transform);
        Destroy(skill_grids, 2f);
        yield return new WaitForSeconds(delay);
        Add_Force(Wolf_Heading_Vec(), dash_speed);
        sound.SKill_1_3_Sound_Play();
        GameObject effectsss = Instantiate(effect.skill_1_2_effect, transform.position + new Vector3(0f, 3f, 2f), Quaternion.identity);
        Destroy(effectsss, 2f);
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
