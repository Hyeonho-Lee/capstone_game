using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move,
        Combat
    }

    public State state;

    private float move_speed;
    private float target_distance;

    private float cooltime;
    private float real_time;
    private bool is_cool;

    private bool is_move_lock;

    private float search_distance;
    private float combat_distance;

    [SerializeField]
    private bool is_move;

    public GameObject base_attack_object;
    public GameObject attack_grid;
    public GameObject attack_grids;

    private GameObject target;
    private Rigidbody rigidbody;

    void Start()
    {
        state = State.Idle;
        target = GameObject.Find("Player");
        rigidbody = GetComponent<Rigidbody>();
        Reset_Status();
    }

    void Update()
    {
        target_distance = Vector3.Distance(transform.position, target.transform.position);

        switch (state) {
            case State.Idle:
                Update_Idle();
                break;
            case State.Move:
                Update_move();
                break;
            case State.Combat:
                Update_combat();
                break;
        }

        if (!is_cool) {
            real_time += Time.deltaTime;

            if (real_time >= cooltime) {
                is_cool = true;
            }
        }
    }

    void Reset_Status()
    {
        search_distance = 23f;
        move_speed = 6.0f;
        combat_distance = 3f;
        cooltime = 8.0f;
        real_time = 2.0f;
    }

    void Update_Idle()
    {
        is_move = false;

        if (search_distance > target_distance && combat_distance < target_distance) {
            state = State.Move;
        }

        if (combat_distance >= target_distance) {
            state = State.Combat;
        }
    }
    void Update_move()
    {
        Move(target, move_speed);
        is_move = true;
        if (search_distance <= target_distance || combat_distance >= target_distance) {
            state = State.Idle;
        }
    }

    void Update_combat()
    {
        is_move = false;
        if (is_cool) {
            Attack_Patern_0();
            StartCoroutine(Is_Grid());
            StartCoroutine(Move_lock(3.5f));
        }
        if (combat_distance <= target_distance && !is_move_lock) {
            state = State.Idle;
        }
    }

    void Attack_Patern_0()
    {
        Instantiate(base_attack_object, this.transform);
        real_time = 0;
        is_cool = false;
    }

    IEnumerator Move_lock(float delay)
    {
        is_move_lock = true;
        yield return new WaitForSeconds(delay);
        is_move_lock = false;
    }

    void Move(GameObject player, float speed)
    {
        Vector3 move_dir = (player.transform.position - transform.position).normalized;
        transform.position += move_dir * speed * Time.deltaTime;
    }

    void Rotate(GameObject player)
    {
        Vector3 result = new Vector3(player.transform.position.x, 0f, player.transform.position.z);
        transform.LookAt(result);
    }

    IEnumerator Is_Grid()
    {
        attack_grid.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        attack_grid.SetActive(false);
        attack_grids.transform.localScale = new Vector3(0.01f, 0.01f, 1f);
    }
}
