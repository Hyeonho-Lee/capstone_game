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

    public float cooltime;
    public float real_time;
    public bool is_cool;

    public GameObject attack_prefab, wait_prefab;
    public GameObject skill_grid_1, skill_grid_2;
    private GameObject player, attack_1, attack_2, attack_3;
    private GameObject grid_1, grid_2, grid_3;
    private GameObject bird;

    private Bird_FSM bird_fsm;
    private GameObject skill_canvas;
    private Boss_Bird_Sound sound;
    private Boss_Bird_Effect effect;

    private Vector3 forward;
    private Vector3 right_45;
    private Vector3 left_45;

    void Start()
    {
        bird = GameObject.Find("Bird_Patern");
        skill_canvas = GameObject.Find("Indicators_Canvas");
        bird_fsm = bird.GetComponent<Bird_FSM>();
        sound = bird.GetComponent<Boss_Bird_Sound>();
        effect = bird.GetComponent<Boss_Bird_Effect>();
        cooltime = 45.0f;
        real_time = 10.0f;
    }

    void Update()
    {
        if (is_attack_1) {
            attack_1.transform.position += forward * move_speed * Time.deltaTime;
            attack_2.transform.position += right_45 * move_speed * Time.deltaTime;
            attack_3.transform.position += left_45 * move_speed * Time.deltaTime;
            grid_1.transform.position += forward * move_speed * Time.deltaTime;
            grid_2.transform.position += right_45 * move_speed * Time.deltaTime;
            grid_3.transform.position += left_45 * move_speed * Time.deltaTime;
        }

        if (!is_cool)       //Ãß°¡
        {
            real_time += Time.deltaTime;
            if (real_time >= cooltime) {
                is_cool = true;
            }
        }
    }

    IEnumerator Is_Attack(float delay)
    {
        is_attack_1 = true;

        player = GameObject.Find("Player").gameObject;
        forward = (player.transform.position - this.transform.position).normalized;
        forward = new Vector3(forward.x, 0f, forward.z);
        right_45 = Quaternion.Euler(0f, 45f, 0f) * forward;
        left_45 = Quaternion.Euler(0f, 315f, 0f) * forward;

        attack_1 = Instantiate(attack_prefab, this.transform);
        GameObject effect_1 = Instantiate(effect.skill_1_1_effect, attack_1.transform);
        effect_1.transform.position += new Vector3(0f, 1.6f, 0f);
        Destroy(effect_1, 3f);
        grid_1 = Instantiate(skill_grid_2, attack_1.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        grid_1.transform.SetParent(skill_canvas.transform);
        Destroy(grid_1, 5f);
        attack_2 = Instantiate(attack_prefab, this.transform);
        GameObject effect_2 = Instantiate(effect.skill_1_1_effect, attack_2.transform);
        effect_2.transform.position += new Vector3(0f, 1.6f, 0f);
        Destroy(effect_2, 3f);
        grid_2 = Instantiate(skill_grid_2, attack_2.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        grid_2.transform.SetParent(skill_canvas.transform);
        Destroy(grid_2, 5f);
        attack_3 = Instantiate(attack_prefab, this.transform);
        GameObject effect_3 = Instantiate(effect.skill_1_1_effect, attack_3.transform);
        effect_3.transform.position += new Vector3(0f, 1.6f, 0f);
        Destroy(effect_3, 3f);
        grid_3 = Instantiate(skill_grid_2, attack_3.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        grid_3.transform.SetParent(skill_canvas.transform);
        Destroy(grid_3, 5f);
        sound.Sound_Play(sound.skill_1_1_sound);
        yield return new WaitForSeconds(delay);
        is_attack_1 = false;
    }
    IEnumerator Is_Wait(float delay)
    {
        Instantiate(wait_prefab, this.transform);
        GameObject wait_effect = Instantiate(effect.skill_1_effect, this.transform);
        wait_effect.transform.position += new Vector3(0f, 1.6f, 0f);
        Destroy(wait_effect, 3f);
        GameObject grid = Instantiate(skill_grid_1, wait_effect.transform.position - new Vector3(0f, 0.1f, 0f), Quaternion.identity);
        grid.transform.SetParent(skill_canvas.transform);
        Destroy(grid, 3f);
        sound.Sound_Play(sound.skill_1_sound);
        yield return new WaitForSeconds(delay);
        StartCoroutine(Is_Attack(move_time));
    }

    [ContextMenu("Attack")]
    public void Attack()
    {
        if (is_cool && bird_fsm.is_attack_1) {
            StartCoroutine(Is_Wait(wait_time));
            real_time = 0;
            is_cool = false;
        }
    }
}
