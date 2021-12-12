using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Monkey_3 : MonoBehaviour
{
    public bool is_cool;

    public bool is_puzzle;
    public bool is_puzzle_clear;
    private bool is_ani;

    private bool is_sound;

    public GameObject puzzle_create;
    public GameObject attack_prefab;
    public GameObject skill_grid;

    private GameObject player;
    private GameObject attacks;
    private GameObject skill;
    private GameObject grid;
    private GameObject monkey;
    public GameObject puzzlesss;

    private Monkey_FSM monkey_fsm;
    private GameObject skill_canvas;
    private Boss_Monkey boss_monkey;
    private Boss_Monkey_Sound sound;
    private Boss_Monkey_Effect effect;

    void Start()
    {
        monkey = GameObject.Find("Monkey_Patern");
        boss_monkey = monkey.GetComponent<Boss_Monkey>();
        skill_canvas = GameObject.Find("Indicators_Canvas");
        monkey_fsm = monkey.GetComponent<Monkey_FSM>();
        sound = monkey.GetComponent<Boss_Monkey_Sound>();
        effect = monkey.GetComponent<Boss_Monkey_Effect>();
    }

    void Update()
    {
        if (is_puzzle) {
            StartCoroutine(Is_Sound(2.0f));
        }

        if (is_ani) {
            StartCoroutine(boss_monkey.Animation_Delay(0.0f, "monkey_charge"));
            is_ani = false;
        }

        if (is_puzzle_clear && is_puzzle) {
            is_puzzle = false;
            is_puzzle_clear = false;
            monkey_fsm.is_attack_lock = false;
            Destroy(grid);
            Destroy(attacks);
            Destroy(skill);
            StartCoroutine(monkey_fsm.attack_lock(5.5f));
            StartCoroutine(boss_monkey.Animation_Delay(0.0f, "monkey_stun"));
            sound.Sound_Play(sound.wall_sound);
        }
    }

    IEnumerator Attack_Spawn(GameObject attack_object, float delay)
    {
        yield return new WaitForSeconds(delay);
        attacks = Instantiate(attack_object, this.transform);
    }

    [ContextMenu("Attack")]
    public void Attack()
    {
        if (is_cool && monkey_fsm.is_attack_3) {
            is_ani = true;

            player = GameObject.Find("Player").gameObject;
            StartCoroutine(Attack_Spawn(attack_prefab, 0f));

            skill = Instantiate(effect.skill_3_effect, monkey.transform);
            grid = Instantiate(skill_grid, this.transform.position, this.transform.rotation);
            grid.transform.SetParent(skill_canvas.transform);

            StartCoroutine(Create_Puzzle(0f));

            monkey_fsm.is_attack_lock = true;
            monkey.transform.position = new Vector3(-150f, 0.5f, 550f);
            is_cool = false;
        }
    }

    IEnumerator Is_Sound(float delay)
    {
        if (!is_sound) {
            is_sound = true;
            sound.Sound_Play(sound.skill_3_sound);
            yield return new WaitForSeconds(delay);
            is_sound = false;
        }
    }

    public IEnumerator Create_Puzzle(float delay)
    {
        puzzlesss = Instantiate(puzzle_create);
        puzzlesss.transform.position = new Vector3(-150f, 2.25f, 550.0f);
        yield return null;
    }
}
