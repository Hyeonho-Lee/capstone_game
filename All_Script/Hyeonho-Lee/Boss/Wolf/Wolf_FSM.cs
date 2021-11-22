using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_FSM : MonoBehaviour
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
    private float target_distance;

    private float cooltime;                 //기본공격 쿨타임
    private float real_time;                //
    private bool is_cool;                   //기본공격 쿨타임이 돌았으면 체크

    private float search_distance;
    private float combat_distance;

    public bool is_move;
    public bool is_attack_0;
    public bool is_attack_1;
    public bool is_attack_2;
    public bool is_attack_3;
    public bool is_attack_lock;

    public GameObject base_attack_object;   //평타 나갈때 박스
    public GameObject skill_grid;

    private GameObject target;              //wolf 공격대상
    private GameObject skill_canvas;
    private Rigidbody rigidbody;
    private Boss_Move boss_move;
    private Boss_Wolf_1 patern_1;
    private Boss_Wolf_2 patern_2;
    private Boss_Wolf_3 patern_3;
    private Boss_Wolf boss_wolf;
    private Boss_Wolf_Effect wolf_effect;

    void Start()
    {
        state = State.Idle;
        target = GameObject.Find("Player");
        skill_canvas = GameObject.Find("Indicators_Canvas");
        boss_move = GetComponent<Boss_Move>();
        rigidbody = GetComponent<Rigidbody>();
        wolf_effect = GetComponent<Boss_Wolf_Effect>();
        patern_1 = GameObject.Find("Wolf_Patern_1").GetComponent<Boss_Wolf_1>();
        patern_2 = GameObject.Find("Wolf_Patern_2").GetComponent<Boss_Wolf_2>();
        patern_3 = GameObject.Find("Wolf_Patern_3").GetComponent<Boss_Wolf_3>();
        boss_wolf = GameObject.Find("Wolf_Patern").GetComponent<Boss_Wolf>();
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

        if (!is_cool) {                     //평타 is cool
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
        search_distance = 23f;
        combat_distance = 6f;
        move_speed = 12.0f;
        cooltime = 6.0f;
        real_time = 4.0f;
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
        if (!is_attack_lock) {
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

        if (combat_distance <= target_distance && !is_attack_lock) {
            state = State.Move;
        }

        if (is_cool) {
            is_attack_0 = true;
            state = State.Skill;
        }

        if (patern_1.is_cool) {
            is_attack_1 = true;
            state = State.Skill;
        }
        if (patern_2.is_cool) {
            is_attack_2 = true;
            state = State.Skill;
        }
        if (patern_3.is_cool) {
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

        if (is_attack_0 && !is_attack_lock) {
            Attack_Patern_0();
            is_attack_0 = false;
            StartCoroutine(attack_lock(3.5f));
        }else if (is_attack_3 && !is_attack_lock) {
            Attack_Patern_3();
            is_attack_3 = false;
            StartCoroutine(attack_lock(5.5f));
        } else if (is_attack_1 && !is_attack_lock) {
            Attack_Patern_1();
            is_attack_1 = false;
            StartCoroutine(attack_lock(8.5f));
        } else if (is_attack_2 && !is_attack_lock) {
            Attack_Patern_2();
            is_attack_2 = false;
            StartCoroutine(attack_lock(17.0f));
        }

    }

    void Update_Out()
    {
        if (search_distance >= target_distance) {
            state = State.Idle;
        }

        if (patern_2.is_cool && !is_attack_lock && !patern_1.is_cool && !is_cool ||
            patern_2.is_cool && !is_attack_lock && !patern_1.is_cool && is_cool ||
            patern_2.is_cool && !is_attack_lock && patern_1.is_cool && is_cool) {
            is_attack_2 = true;
            state = State.Skill;
        }

        if (patern_3.is_cool) {
            is_attack_3 = true;
            state = State.Skill;
        }
    }

    void Attack_Patern_0()
    {
        Instantiate(base_attack_object, this.transform);
        real_time = 0;
        is_cool = false;
        GameObject grid = Instantiate(skill_grid, this.transform.position, this.transform.rotation);
        grid.transform.SetParent(skill_canvas.transform);
        Destroy(grid, 3f);
        StartCoroutine(boss_wolf.Animation_Delay(2.0f, "base_attack"));
        StartCoroutine(base_effect(2.0f));
    }

    void Attack_Patern_1()
    {
        patern_1.Attack();
        StartCoroutine(boss_wolf.Animation_Delay(2.0f, "skill_1"));
        StartCoroutine(boss_wolf.Animation_Delay(4.0f, "skill_1"));
        StartCoroutine(boss_wolf.Animation_Delay(6.0f, "skill_1"));
    }

    void Attack_Patern_2()
    {
        patern_2.Attack();
        StartCoroutine(boss_wolf.Animation_Delay(3.0f, "skill_2-1"));
        StartCoroutine(boss_wolf.Animation_Delay(8.0f, "skill_2-2"));
        StartCoroutine(boss_wolf.Animation_Delay(11.0f, "skill_2-3"));
        StartCoroutine(boss_wolf.Animation_Delay(13.0f, "skill_2-4"));
    }

    void Attack_Patern_3()
    {
        patern_3.Attack();
        StartCoroutine(boss_wolf.Animation_Delay(0.5f, "skill_3"));
    }

    IEnumerator attack_lock(float delay)
    {
        is_attack_lock = true;
        yield return new WaitForSeconds(delay);
        is_attack_lock = false;
    }

    IEnumerator base_effect(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject effect = Instantiate(wolf_effect.skill_0_effect, this.transform.position + new Vector3(0f, 2f, 0f), this.transform.rotation * Quaternion.Euler(90f, 0f, 0f));
        effect.transform.parent = this.transform;
        Destroy(effect, 1f);
    }
}
