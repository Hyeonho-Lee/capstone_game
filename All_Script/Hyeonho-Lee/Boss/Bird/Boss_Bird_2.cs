using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bird_2 : MonoBehaviour
{
    public float spawn_delay;
    public float count;

    public float real_time;
    public float cooltime;
    public bool is_cool;

    public Vector3 range;
    private Vector3 random_range;

    public GameObject attack_prefab;
    public GameObject skill_grid;
    private GameObject player;
    private GameObject spawn_attack;

    private GameObject bird;
    private GameObject skill_canvas;
    private Bird_FSM bird_fsm;
    private Boss_Bird_Sound sound;
    private Boss_Bird_Effect effect;

    void Start()
    {
        bird = GameObject.Find("Bird_Patern");
        skill_canvas = GameObject.Find("Indicators_Canvas");
        bird_fsm = bird.GetComponent<Bird_FSM>();
        sound = bird.GetComponent<Boss_Bird_Sound>();
        effect = bird.GetComponent<Boss_Bird_Effect>();

        cooltime = 60.0f;
        real_time = 10.0f;
        count = 30;
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

    IEnumerator Random_Spawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        random_range = new Vector3(Random.Range(player.transform.position.x - range.x, player.transform.position.x + range.x), 0f, Random.Range(player.transform.position.z - range.z, player.transform.position.z + range.z));
        spawn_attack = Instantiate(attack_prefab);
        spawn_attack.transform.position = random_range;
        GameObject grid = Instantiate(skill_grid, spawn_attack.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        grid.transform.SetParent(skill_canvas.transform);
        Destroy(grid, 3f);
        StartCoroutine(Effect_Play(3.0f, spawn_attack.transform));
    }
    IEnumerator Effect_Play(float delay, Transform position)
    {
        yield return new WaitForSeconds(delay);
        GameObject skill_2 = Instantiate(effect.skill_2_effect, position);
        skill_2.transform.position = position.position;
        Destroy(skill_2, 2f);
        sound.Sound_Play(sound.skill_2_sound);
    }

    [ContextMenu("Attack")]
    public void Attack()
    {
        player = GameObject.Find("Player").gameObject;
        if (is_cool && bird_fsm.is_attack_2) {
            for (int i = 0; i < count; i++) {
                StartCoroutine(Random_Spawn(spawn_delay * i));
            }
            real_time = 0;
            is_cool = false;
        }
    }
}
