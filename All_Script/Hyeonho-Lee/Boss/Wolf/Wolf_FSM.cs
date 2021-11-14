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

    public float move_speed;
    public float target_distance;

    public float cooltime;
    public float real_time;
    public bool is_cool;

    private float search_distance;
    private float combat_distance;

    public bool is_move;
    public bool is_attack_0;
    public bool is_attack_1;
    public bool is_attack_2;
    public bool is_attack_3;
    public bool is_attack_lock;

    public GameObject base_attack_object;

    private GameObject target;
    private Boss_Move boss_move;
    private Boss_Wolf_1 patern_1;
    private Boss_Wolf_2 patern_2;
    private Boss_Wolf_3 patern_3;

    void Start()
    {
        state = State.Idle;
        target = GameObject.Find("Player");
        boss_move = GetComponent<Boss_Move>();
        Reset_Status();
        patern_1 = GameObject.Find("Wolf_Patern_1").GetComponent<Boss_Wolf_1>();
        patern_2 = GameObject.Find("Wolf_Patern_2").GetComponent<Boss_Wolf_2>();
        patern_3 = GameObject.Find("Wolf_Patern_3").GetComponent<Boss_Wolf_3>();
    }

    void Update()
    {
        target_distance = Vector3.Distance(transform.position, target.transform.position);
        boss_move.Rotate(target);

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
            case State.Move:
                Update_Move();
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

    void Reset_Status()
    {
        search_distance = 23f;
        combat_distance = 6f;
        move_speed = 12f;
    }

    void Update_Idle()
    {
        if (search_distance > target_distance) {
            state = State.Move;
        }

        if (search_distance + 5.0f <= target_distance) {
            state = State.Out;
        }
    }

    void Update_Move()
    {
        if (search_distance <= target_distance) {
            state = State.Idle;
        }

        if (combat_distance > target_distance) {
            state = State.Combat;
        }
    }

    void Update_Combat()
    {
        if (combat_distance <= target_distance) {
            state = State.Move;
        }

        if (is_cool && !is_attack_lock && !patern_1.is_cool)  {
            is_attack_0 = true;
            state = State.Skill;
        } 
        
        if (patern_1.is_cool && !is_attack_lock && is_cool ||
            patern_1.is_cool && !is_attack_lock && !is_cool) {
            is_attack_1 = true;
            state = State.Skill;
        }

        if (patern_3.is_cool) {
            is_attack_3 = true;
            state = State.Skill;
        }
    }

    void Update_Skill()
    {
        if (search_distance <= target_distance) {
            state = State.Idle;
        }

        if (search_distance + 5.0f > target_distance) {
            state = State.Move;
        }

        if (combat_distance > target_distance) {
            state = State.Combat;
        }

        if (is_attack_0) {
            Attack_Patern_0();
            StartCoroutine(attack_lock(3.5f));
            is_attack_0 = false;
        }

        if (is_attack_1) {
            Attack_Patern_1();
            StartCoroutine(attack_lock(8.5f));
            is_attack_1 = false;
        }

        if (is_attack_2) {
            Attack_Patern_2();
            StartCoroutine(attack_lock(15.5f));
            is_attack_2 = false;
        }

        if (is_attack_3) {
            Attack_Patern_3();
            StartCoroutine(attack_lock(5.5f));
            is_attack_3 = false;
        }
    }

    void Update_Out()
    {
        if (search_distance  >= target_distance) {
            state = State.Idle;
        }

        if (patern_2.is_cool && !is_attack_lock && !patern_1.is_cool ||
            patern_2.is_cool && !is_attack_lock && patern_1.is_cool) {
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
    }

    void Attack_Patern_1()
    {
        patern_1.Attack();
    }

    void Attack_Patern_2()
    {
        patern_2.Attack();
    }

    void Attack_Patern_3()
    {
        patern_3.Attack();
    }

    IEnumerator attack_lock(float delay)
    {
        is_attack_lock = true;
        yield return new WaitForSeconds(delay);
        is_attack_lock = false;
    }
}
