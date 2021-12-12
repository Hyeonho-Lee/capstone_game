using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement P;

    public float h_axis, v_axis;
    public float move_speed;
    public float rotate_speed;
    public float dash_speed;
    public float dash_time;
    public float attack_time;
    public float infinity_time;
    public float stamina;
    public float health;

    private float speed_backup;

    public bool is_move;
    public bool is_dash;
    public bool is_attack;
    public bool is_defence;
    public bool is_damage;
    public bool is_defence_damage;
    public bool is_infinity;
    public bool is_pick;
    public bool is_skill;
    public bool is_talk;
    public bool is_inventory;
    public bool is_stamina;
    public bool is_die;
    public bool is_test;
    public bool lock_move;
    public bool lock_attack;
    public bool lock_dash;
    public bool attack_start;

    public Vector3 movement_vector;
    private Vector3 dash_vector;
    private Vector3 camera_forward, hit_position;

    public GameObject defence_object;
    public GameObject attack_object_1;
    public GameObject attack_object_2;
    public GameObject attack_object_3;
    public GameObject skill_object_3;
    public GameObject skill_object_4;
    public Slider stamina_bar;
    public Material damage_mat;
    private Material object_mat;
    private Material hat_mat;
    private Material weapon_mat;
    private Material weaponslot_mat;

    private Rigidbody rigidbody;
    private PlayerStatus player_status;
    private Player_Attack player_attack;
    private Player_Skill player_skill;
    private PlayerEffect player_effect;
    private UI_Inventory ui_inventory;
    private Health_Controller health_controller;
    private PlayerSound player_sound;
    private World_Admin world_admin;
    private Loading_Scene loading_scene;
    private NPC_Manager npc_manager;

    private CapsuleCollider collider;
    private Animator animator;
    private Animator hit_animator;
    private Animator die_animator;

    void Start()
    {
        P = this;

        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        player_status = GameObject.Find("System").GetComponent<PlayerStatus>();
        ui_inventory = GameObject.Find("System").GetComponent<UI_Inventory>();
        health_controller = GameObject.Find("System").GetComponent<Health_Controller>();
        world_admin = GameObject.Find("System").GetComponent<World_Admin>();
        loading_scene = GameObject.Find("System").GetComponent<Loading_Scene>();
        hit_animator = GameObject.Find("hit_effect").GetComponent<Animator>();
        die_animator = GameObject.Find("die_effect").GetComponent<Animator>();
        player_attack = GetComponent<Player_Attack>();
        player_skill = GetComponent<Player_Skill>();
        player_effect = GetComponent<PlayerEffect>();
        player_sound = GetComponent<PlayerSound>();

        animator = GetComponent<Animator>();
        player_status.Stat_Load();
        Reset_Status();
        health_controller.Check_Health(health);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Boss"));
    }

    void Update()
    {
        Input_Key();
        Check_Value();
    }

    void FixedUpdate()
    {
        if (!is_attack) 
        {
            Move(h_axis, v_axis);
        }

        if (is_dash) {
            lock_move = true;
            Add_Force(dash_vector, dash_speed);
        } else {
            lock_move = false;
        }

        rigidbody.AddForce(Vector3.down * 300f);
    }

    void Reset_Status()
    {
        health = player_status.player_stat.health;
        move_speed = player_status.player_stat.move_speed;
        speed_backup = move_speed;
        rotate_speed = 20.0f;
        dash_speed = 1500.0f;
        dash_time = 0.35f;
        infinity_time = 0.35f;
        stamina = 100.0f;
        is_stamina = true;
    }


    void Input_Key()
    {
        if (!is_talk)
        {
            h_axis = Input.GetAxisRaw("Horizontal");
            v_axis = Input.GetAxisRaw("Vertical");
        }

        camera_forward = Camera.main.transform.forward;
        camera_forward.y = 0;
        camera_forward = Vector3.Normalize(camera_forward);

        if (!lock_attack && !is_dash && !is_pick && !is_skill && !is_talk && !is_inventory && Input.GetMouseButtonDown(0) && !player_skill.is_heal && !is_die) {
            StartCoroutine(Attack(0.5f));
            player_attack.Attack();
        }

        if (!is_attack && !is_dash && !is_pick && !is_skill && !is_talk && !is_inventory && Input.GetMouseButtonDown(1) && stamina >= 0 && !player_skill.is_heal && !is_die) {
            is_defence = true;
        }

        if (Input.GetMouseButtonUp(1)) {
            is_defence = false;
        }

        if (!is_attack && !is_defence && !lock_dash && !is_pick && !is_skill && !is_talk && !is_inventory && Input.GetKeyDown(KeyCode.Space) && !player_skill.is_heal && !is_die) {
            StartCoroutine(Dash(dash_time));
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        if (is_talk && Input.GetKeyDown(KeyCode.Space)) {
            npc_manager = GameObject.Find("System").GetComponent<NPC_Manager>();
            npc_manager.Next_Sentence();
        }

        if (!is_talk && Input.GetKeyDown(KeyCode.Tab) && !is_die) {
            //Debug.Log("인벤토리 염");
            if (is_inventory) {
                ui_inventory.Inventory_Exit();
            }else {
                ui_inventory.Inventory_Start();
            }
        }

        if (!is_talk && !is_inventory && Input.GetKeyDown(KeyCode.Alpha1) && !is_die) {
            StartCoroutine(player_skill.Skill_1());
        }

        if (!is_talk && !is_inventory && Input.GetKeyDown(KeyCode.Alpha2) && !is_die) {
            StartCoroutine(player_skill.Skill_2());
        }

        if (!is_talk && !is_inventory && Input.GetKeyDown(KeyCode.Alpha3) && !is_die) {
            StartCoroutine(player_skill.Skill_3());
        }

        if (!is_talk && !is_inventory && Input.GetKeyDown(KeyCode.Alpha4) && !is_die) {
            StartCoroutine(player_skill.Skill_4());
        }
    }

    void Check_Value()
    {
        if (h_axis == 0 && v_axis == 0)
            is_move = false;
        else
            is_move = true;

        if (lock_move || is_attack || is_pick || is_skill || is_talk || is_inventory || player_skill.is_heal || is_die) {
            h_axis = 0;
            v_axis = 0;
        }

        if (is_dash && is_talk || is_dash && is_pick || is_attack && is_talk || is_attack && is_pick) {
            is_dash = false;
            is_attack = false;
        }

        if (is_defence) {
            lock_attack = true;
            move_speed = 0;
            defence_object.gameObject.SetActive(true);
        }else {
            lock_attack = false;
            move_speed = speed_backup;
            defence_object.gameObject.SetActive(false);
        }

        if (is_stamina && stamina <= 100.0f) {
            if (is_defence) {
                stamina += Time.deltaTime * 2.5f;
            } else if (player_skill.is_buff) {
                stamina += Time.deltaTime * 7.5f;
            } else {
                stamina += Time.deltaTime * 5f;
            }
        }

        if (!is_stamina && stamina <= 0) {
            is_stamina = true;
        }

        if (stamina_bar != null) {
            stamina_bar.value = stamina / 100;
        }

        if (health <= 0 && !is_test) {
            StartCoroutine(Is_Die());
        }else if (health <= 0 && is_test){
            health += 10;
        }

        if (player_skill.is_buff && !is_defence) {
            move_speed = 20.0f;
        }else if (!player_skill.is_buff && !is_defence) {
            move_speed = speed_backup;
        }
    }

    void Move(float horizontal, float vertical)
    {
        movement_vector = new Vector3(h_axis, 0, v_axis).normalized;
        rigidbody.velocity = movement_vector * move_speed;
        //transform.position += movement_vector * move_speed * Time.deltaTime;
        Move_Turn();
    }

    void Move_Turn()
    {
        if (h_axis == 0 && v_axis == 0)
            return;

        Quaternion newRotation = Quaternion.LookRotation(movement_vector * rotate_speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotate_speed * Time.deltaTime);
    }

    void Mouse_Watch()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //가상의 바닥으로 회전
        Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;
        if (GroupPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointTolook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));
        }

        /*
        // 물리적 충돌로 회전
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10000f)) {
            hit_position = hit.point;

            if (is_move) {
                transform.LookAt(new Vector3(hit_position.x, transform.position.y, hit_position.z));
            }else {
                Vector3 hit_vector = hit_position - transform.position;
                Quaternion newRotation = Quaternion.LookRotation(hit_vector * rotate_speed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotate_speed * Time.deltaTime * 2.0f);
            }

            //Debug.DrawRay(cameraRay.origin, hit.point, Color.red, 1f);
        }*/
    }

    IEnumerator Dash(float delay)
    {
        if (!is_dash && stamina >= 0f) {
            is_dash = true;
            lock_attack = true;
            dash_vector = new Vector3(transform.forward.x, 0, transform.forward.z);
            StartCoroutine(Infinity(infinity_time));
            StartCoroutine(player_effect.Dash_Effect());
            stamina -= 25.0f;
            is_stamina = false;
            player_sound.Dash_Sound_Play();
            yield return new WaitForSeconds(delay);
            is_dash = false;
            lock_attack = false;
            is_stamina = true;
        }
    }

    IEnumerator Attack(float delay)
    {
        if (!is_attack) {
            is_attack = true;
            //Mouse_Watch();
            yield return new WaitForSeconds(delay);
        }
    }

    void Add_Force(Vector3 dir, float force)
    {
        rigidbody.AddForce(dir * force, ForceMode.Force);
    }

    /*void OnTriggerStay(Collider other)
    {
        //Debug.Log(other);
        if(other.tag == "Attack")
        {
            if(!is_defence) {
                StartCoroutine(Is_Damage(1.0f));
            }else {
                StartCoroutine(Is_Defence(1.0f));
            }
        }
    }*/

    public void Player_Hit()
    {
        StartCoroutine(Is_Damage(1.0f));
    }

    public void Player_Defence()
    {
        StartCoroutine(Is_Defence(1.0f));
    }

    IEnumerator Is_Damage(float delay)
    {
        if (!is_damage && !is_infinity && !is_die) {
            is_damage = true;
            player_status.Player_Damage();
            //Debug.Log("플레이어 데미지 받음");
            player_sound.Player_Hit_Sound_Play();
            hit_animator.Play("Hit");
            health -= 1f;
            health_controller.Check_Health(health);
            yield return new WaitForSeconds(delay);
            is_damage = false;
        }
    }

    IEnumerator Is_Defence(float delay)
    {
        if (!is_defence_damage && !is_die) {
            is_defence_damage = true;
            if (stamina >= 20.0f) {
                //Debug.Log("쉴드 데미지 받음");
                player_sound.Shield_Sound_Play();
                StartCoroutine(Infinity(1.0f));
                stamina -= 20.0f;
                is_stamina = false;
            } else {
                //Debug.Log("플레이어 데미지 받음");
                player_sound.Shield_Sound_Play();
                StartCoroutine(Infinity(1.0f));
                stamina -= 20.0f;
                is_defence = false;
                is_stamina = false;
            }
            yield return new WaitForSeconds(delay);
            is_defence_damage = false;
            is_stamina = true;
        }
    }

    IEnumerator Infinity(float delay)
    {
        if (!is_infinity && !is_die) {
            is_infinity = true;
            yield return new WaitForSeconds(delay);
            is_infinity = false;
        }
    }

    IEnumerator Is_Die()
    {
        if (!is_die && !is_test) {
            is_die = true;
            animator.Play("dieing");
            Debug.Log("유다희");
            yield return new WaitForSeconds(3.0f);
            die_animator.Play("die_effect");
            yield return new WaitForSeconds(6.0f);
            world_admin.Load_Scene(0);
        }
    }
}
