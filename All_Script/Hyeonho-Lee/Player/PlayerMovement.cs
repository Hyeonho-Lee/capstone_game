using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float h_axis, v_axis;
    public float move_speed;
    public float rotate_speed;
    public float dash_speed;
    public float dash_time;
    public float attack_time;
    public float infinity_time;
    public float stamina;
    public float health = 10f;

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
    public AudioClip hit_sound;
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
    private Renderer renderer;
    private Renderer renderer_1;
    private Renderer renderer_2;
    private Renderer renderer_3;
    private CapsuleCollider collider;
    private Animator animator;
    private AudioSource audio;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        //player_status = GameObject.Find("System").GetComponent<PlayerStatus>();
        ui_inventory = GameObject.Find("System").GetComponent<UI_Inventory>();
        health_controller = GameObject.Find("System").GetComponent<Health_Controller>();
        player_attack = GetComponent<Player_Attack>();
        player_skill = GetComponent<Player_Skill>();
        player_effect = GetComponent<PlayerEffect>();
        audio = GetComponent<AudioSource>();
        renderer = GameObject.Find("Player_Renderer").GetComponent<Renderer>();
        renderer_1 = GameObject.Find("hat").GetComponent<Renderer>();
        renderer_2 = GameObject.Find("weapon").GetComponent<Renderer>();
        renderer_3 = GameObject.Find("weapon_slot").GetComponent<Renderer>();
        animator = GetComponent<Animator>();
        object_mat = renderer.material;
        hat_mat = renderer_1.material;
        weapon_mat = renderer_2.material;
        weaponslot_mat = renderer_3.material;
        //player_status.Stat_Load();
        Reset_Status();
        health_controller.Check_Health(health);
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
        move_speed = 15.0f;
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

        if (!lock_attack && !is_dash && !is_pick && !is_skill && !is_talk && !is_inventory && Input.GetMouseButtonDown(0)) {
            StartCoroutine(Attack(0.5f));
            player_attack.Attack();
        }

        if (!is_attack && !is_dash && !is_pick && !is_skill && !is_talk && !is_inventory && Input.GetMouseButtonDown(1) && stamina >= 0) {
            is_defence = true;
        }

        if (Input.GetMouseButtonUp(1)) {
            is_defence = false;
        }

        if (!is_attack && !is_defence && !lock_dash && !is_pick && !is_skill && !is_talk && !is_inventory && Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(Dash(dash_time));
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        if (!is_talk && Input.GetKeyDown(KeyCode.Tab)) {
            //Debug.Log("인벤토리 염");
            if (is_inventory) {
                ui_inventory.Inventory_Exit();
            }else {
                ui_inventory.Inventory_Start();
            }
        }

        if (!is_talk && !is_inventory && Input.GetKeyDown(KeyCode.Alpha1)) {
            StartCoroutine(player_skill.Skill_1());
        }

        if (!is_talk && !is_inventory && Input.GetKeyDown(KeyCode.Alpha2)) {
            StartCoroutine(player_skill.Skill_2());
        }

        if (!is_talk && !is_inventory && Input.GetKeyDown(KeyCode.Alpha3)) {
            StartCoroutine(player_skill.Skill_3());
        }

        if (!is_talk && !is_inventory && Input.GetKeyDown(KeyCode.Alpha4)) {
            StartCoroutine(player_skill.Skill_4());
        }
    }

    void Check_Value()
    {
        if (h_axis == 0 && v_axis == 0)
            is_move = false;
        else
            is_move = true;

        if (lock_move || is_attack || is_pick || is_skill || is_talk || is_inventory) {
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

        if (is_damage) {
            renderer.material = damage_mat;
            renderer_1.material = damage_mat;
            renderer_2.material = damage_mat;
            renderer_3.material = damage_mat;
        } else {
            renderer.material = object_mat;
            renderer_1.material = hat_mat;
            renderer_2.material = weapon_mat;
            renderer_3.material = weaponslot_mat;
        }

        if (is_stamina && stamina <= 100.0f) {
            if(is_defence) {
                stamina += Time.deltaTime * 2.5f;
            }else {
                stamina += Time.deltaTime * 5f;
            }
        }

        if (!is_stamina && stamina <= 0) {
            is_stamina = true;
        }

        if (stamina_bar != null) {
            stamina_bar.value = stamina / 100;
        }

        if (health <= 0) {
            Debug.Log("You Die");
            health += 10;
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

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Attack")
        {
            if(!is_defence) {
                StartCoroutine(Is_Damage(1.0f));
            }else {
                StartCoroutine(Is_Defence(1.0f));
            }
        }
    }

    IEnumerator Is_Damage(float delay)
    {
        if (!is_damage && !is_infinity) {
            is_damage = true;
            //player_status.Player_Damage();
            Debug.Log("플레이어 데미지 받음");
            audio.PlayOneShot(hit_sound);
            health -= 1f;
            health_controller.Check_Health(health);
            yield return new WaitForSeconds(delay);
            is_damage = false;
        }
    }

    IEnumerator Is_Defence(float delay)
    {
        if (!is_defence_damage) {
            is_defence_damage = true;
            if (stamina >= 40.0f) {
                Debug.Log("쉴드 데미지 받음");
                stamina -= 40.0f;
                is_stamina = false;
            } else {
                Debug.Log("플레이어 데미지 받음");
                stamina -= 40.0f;
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
        if (!is_infinity) {
            is_infinity = true;
            yield return new WaitForSeconds(delay);
            is_infinity = false;
        }
    }
}
