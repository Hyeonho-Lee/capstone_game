using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey_FSM : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move,
        Combat,
        Skill,
        Out
    }

    public State state;

    private float move_speed;
    public float target_distance;

    private float cooltime;
    private float real_time;
    private bool is_cool;

    private float search_distance;
    private float combat_distance;

    public bool is_move;
    public bool is_attack_0;
    public bool is_attack_1;
    public bool is_attack_2;
    public bool is_attack_3;
    public bool is_attack_lock;

    public GameObject base_attack_object;
    public GameObject skill_grid;

    private GameObject target;
    private GameObject skill_canvas;
    private Rigidbody rigidbody;
    private Boss_Move boss_move;
    private Boss_Monkey_1 pattern_1;
    private Boss_Monkey_2 pattern_2;
    private Boss_Monkey_3 pattern_3;
    private Boss_Monkey boss_monkey;
    private Boss_Monkey_Effect effect;
    private Boss_Monkey_Sound sound;

    void Start()
    {
        state = State.Idle;
        target = GameObject.Find("Player");
        skill_canvas = GameObject.Find("Indicators_Canvas");
        boss_move = GetComponent<Boss_Move>();
        rigidbody = GetComponent<Rigidbody>();
        boss_monkey = GetComponent<Boss_Monkey>();
        effect = GetComponent<Boss_Monkey_Effect>();
        sound = GetComponent<Boss_Monkey_Sound>();
        pattern_1 = GameObject.Find("Monkey_Patern_1").GetComponent<Boss_Monkey_1>();
        pattern_2 = GameObject.Find("Monkey_Patern_2").GetComponent<Boss_Monkey_2>();
        pattern_3 = GameObject.Find("Monkey_Patern_3").GetComponent<Boss_Monkey_3>();
        Reset_Status();
    }

    void Update()
    {
        skill_canvas.transform.position = this.transform.position;
        target_distance = Vector3.Distance(transform.position, target.transform.position);

        if (is_attack_lock) {
            rigidbody.mass = 1;             //질량
        } else {
            rigidbody.mass = 100;           //안밀리게 하기 위함. 
            boss_move.Rotate(target);
        }

        if (!is_cool) {
            real_time += Time.deltaTime;
            if (real_time >= cooltime) {
                is_cool = true;
            }
        }

        switch (state) {
            case State.Idle:
                Update_Idle();
                break;
            case State.Combat:
                Update_Combat();
                break;
            case State.Skill:
                Update_Skill();
                break;
            case State.Out:
                Update_Out();
                break;
        }
    }

    void FixedUpdate()
    {
        switch (state) {
            case State.Move:
                Update_Move();
                break;
        }
    }

    void Reset_Status()
    {
        search_distance = 42f;
        combat_distance = 7f;
        move_speed = 14.0f;
        cooltime = 5.0f;
        real_time = 0.0f;
    }

    void Update_Idle()
    {
        is_move = false;

        if (search_distance > target_distance) {
            state = State.Move;
        }

        if (search_distance + 5.0f <= target_distance) {
            state = State.Out;
        }
    }

    void Update_Move()
    {
        if (!is_attack_lock && !pattern_1.is_area) {
            boss_move.Move(target, move_speed);
            is_move = true;
        }

        if (search_distance <= target_distance) {
            state = State.Idle;
        }

        if (combat_distance > target_distance) {
            state = State.Combat;
        }
    }

    void Update_Combat()
    {
        is_move = false;

        if (combat_distance <= target_distance & !is_attack_lock) {
            state = State.Move;
        }

        if (is_cool) {
            is_attack_0 = true;
            state = State.Skill;
        }

        if (pattern_1.is_cool) {
            is_attack_1 = true;
            state = State.Skill;
        }

        if (pattern_2.is_cool) {
            is_attack_2 = true;
            state = State.Skill;
        }

        if (pattern_3.is_cool) {
            is_attack_3 = true;
            state = State.Skill;
        }
    }
    void Update_Skill()
    {
        if (search_distance <= target_distance && !is_attack_lock) {
            state = State.Idle;
        }

        if (search_distance + 5.0f > target_distance && !is_attack_lock) {
            state = State.Move;
        }

        if (is_attack_2 && !is_attack_lock && pattern_1.attacks == null) {
            Attack_Pattern_2();
            is_attack_2 = false;
            StartCoroutine(attack_lock(11.5f));
        } else if (is_attack_3 && !is_attack_lock && pattern_1.attacks == null) {
            Attack_Pattern_3();
            is_attack_3 = false;
        } else if (is_attack_0 && !is_attack_lock && pattern_1.attacks == null) {
            Attack_Pattern_0();
            is_attack_0 = false;
            StartCoroutine(attack_lock(3.5f));
        } else if (is_attack_1 && !is_attack_lock && pattern_1.attacks == null) {
            Attack_Pattern_1();
            is_attack_1 = false;
        }
    }

    void Update_Out()
    {
        if (search_distance >= target_distance) {
            state = State.Idle;
        }
    }

    void Attack_Pattern_0()
    {
        Instantiate(base_attack_object, this.transform);
        real_time = 0;
        is_cool = false;
        //GameObject grid = Instantiate(skill_grid, this.transform.position, this.transform.rotation);
        //grid.transform.SetParent(skill_canvas.transform);
        //Destroy(grid, 3f);
        //StartCoroutine(base_effect(2.0f));
        StartCoroutine(boss_monkey.Animation_Delay(2.0f, "monkey_skill_0"));
        //StartCoroutine(sound_delay(2.0f));
    }

    void Attack_Pattern_1()
    {
        pattern_1.Attack();
        //StartCoroutine(boss_bird.Animation_Delay(1.0f, "bird_skill_1"));
    }

    void Attack_Pattern_2()
    {
        pattern_2.Attack();
        StartCoroutine(boss_monkey.Animation_Delay(1.0f, "monkey_skill_2"));
        StartCoroutine(boss_monkey.Animation_Delay(4.0f, "monkey_skill_2"));
        StartCoroutine(boss_monkey.Animation_Delay(7.0f, "monkey_skill_2"));
    }

    void Attack_Pattern_3()
    {
        pattern_3.Attack();
        //StartCoroutine(boss_bird.Animation_Delay(0.0f, "bird_skill_2"));
    }

    public IEnumerator attack_lock(float delay)
    {
        is_attack_lock = true;
        yield return new WaitForSeconds(delay);
        is_attack_lock = false;
    }

    IEnumerator base_effect(float delay)
    {
        yield return new WaitForSeconds(delay);
        /*GameObject effects = Instantiate(effect.base_effect, this.transform.position + new Vector3(0f, 2f, 0f), this.transform.rotation * Quaternion.Euler(90f, 0f, 0f));
        effects.transform.parent = this.transform;
        Destroy(effects, 1f);*/
    }

    IEnumerator sound_delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        //sound.Sound_Play(sound.skill_0_sound);
    }
}
