using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Monkey_2 : MonoBehaviour
{
    public float interval_time;

    public float cooltime;
    public float real_time;
    public bool is_cool;

    public GameObject attack_1;
    public GameObject attack_2;
    public GameObject attack_3;
    public GameObject skill_grid_1;
    public GameObject skill_grid_2;
    public GameObject skill_grid_3;

    private GameObject monkey;

    private Monkey_FSM monkey_fsm;
    private GameObject skill_canvas;
    private Boss_Monkey_Sound sound;
    private Boss_Monkey_Effect effect;

    void Start()
    {
        monkey = GameObject.Find("Monkey_Patern");
        skill_canvas = GameObject.Find("Indicators_Canvas");
        monkey_fsm = monkey.GetComponent<Monkey_FSM>();
        sound = monkey.GetComponent<Boss_Monkey_Sound>();
        effect = monkey.GetComponent<Boss_Monkey_Effect>();
        cooltime = 35.0f;
        real_time = 1.0f;
    }

    void Update()
    {
        if (!is_cool) {
            real_time += Time.deltaTime;
            if (real_time >= cooltime) {
                is_cool = true;
            }
        }
    }

    IEnumerator Attack_Spawn(GameObject attack_object, float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(attack_object, this.transform);
        StartCoroutine(sound_delay(1.0f));
    }

    [ContextMenu("Attack")]
    public void Attack()
    {
        if (is_cool && monkey_fsm.is_attack_2) {
            StartCoroutine(Attack_Spawn(attack_1, 0f));
            StartCoroutine(Grid_Spawn(skill_grid_1, 0f));
            StartCoroutine(Effect_Spawn(effect.skill_2_effect, 0.5f));
            StartCoroutine(Attack_Spawn(attack_2, interval_time));
            StartCoroutine(Grid_Spawn(skill_grid_2, interval_time));
            StartCoroutine(Effect_Spawn(effect.skill_2_1_effect, interval_time));
            StartCoroutine(Attack_Spawn(attack_3, interval_time * 2f));
            StartCoroutine(Grid_Spawn(skill_grid_3, interval_time * 2f));
            StartCoroutine(Effect_Spawn(effect.skill_2_2_effect, interval_time * 2f));
            real_time = 0;
            is_cool = false;
        }
    }

    IEnumerator sound_delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        sound.Sound_Play(sound.skill_2_sound);
    }

    IEnumerator Effect_Spawn(GameObject attack_object, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject effects = Instantiate(attack_object, this.transform);
        effects.transform.position += new Vector3(0.0f, 9.5f, 0.0f);
        Destroy(effects, 3.0f);
    }

    IEnumerator Grid_Spawn(GameObject attack_object, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject grid = Instantiate(attack_object, this.transform.position, this.transform.rotation);
        grid.transform.SetParent(skill_canvas.transform);
        Destroy(grid, 3f);
    }
}
