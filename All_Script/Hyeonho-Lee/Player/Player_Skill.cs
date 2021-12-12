using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_Skill : MonoBehaviour
{
    public int heal_count;
    private float heal_time;
    private float heal_realtime;
    public bool ready_heal;
    public bool is_heal;
    public bool is_buff;

    private float skill_1_time;
    private float skill_1_realtime;
    public bool ready_skill_1;
    public bool is_skill_1;

    private float skill_2_time;
    private float skill_2_realtime;
    public bool ready_skill_2;
    public bool is_skill_2;

    private float skill_3_time;
    private float skill_3_realtime;
    public bool ready_skill_3;
    public bool is_skill_3;

    public GameObject heal_effect;
    public GameObject buff_effect_1;
    public GameObject buff_effect_2;
    public GameObject ui_count;
    public Image heal_cooltime;
    public Image skill_1_cooltime;
    public Image skill_2_cooltime;
    public Image skill_3_cooltime;

    public Sprite skill_1;
    public Sprite skill_2;
    public Sprite skill_3;
    public Sprite skill_4;

    private Vector3 dash_vector;

    private GameObject player;
    private GameObject banner;
    private PlayerMovement player_movement;
    private PlayerSound player_sound;

    private Health_Controller health_controller;
    private Animator animator;
    private Animator skill_animator;
    private Rigidbody rigidbody;
    private TextMeshProUGUI textmesh;
    private PlayerData player_data;

    void Start()
    {
        player = GameObject.Find("Player");
        banner = GameObject.Find("Banner");
        health_controller = GameObject.Find("System").GetComponent<Health_Controller>();
        player_data = GameObject.Find("System").GetComponent<PlayerData>();
        player_movement = player.GetComponent<PlayerMovement>();
        animator = player.GetComponent<Animator>();
        skill_animator = banner.GetComponent<Animator>();
        player_sound = player.GetComponent<PlayerSound>();
        rigidbody = player.GetComponent<Rigidbody>();
        textmesh = ui_count.GetComponent<TextMeshProUGUI>();
        heal_count = 3;
        heal_time = 5.0f;
        heal_realtime = 5.0f;
        skill_1_time = 40.0f;
        skill_1_realtime = 40.0f;
        skill_2_time = 10.0f;
        skill_2_realtime = 10.0f;
        skill_3_time = 20.0f;
        skill_3_realtime = 20.0f;
    }

    void Update()
    {
        if (!ready_heal) {
            if (heal_realtime <= heal_time) {
                heal_realtime += Time.deltaTime;
            }else {
                ready_heal = true;
            }
            heal_cooltime.fillAmount = heal_realtime / heal_time;
        } else {
            heal_cooltime.fillAmount = 0f;
        }

        if (!ready_skill_1) {
            if (skill_1_realtime <= skill_1_time) {
                skill_1_realtime += Time.deltaTime;
            } else {
                ready_skill_1 = true;
            }
            skill_1_cooltime.fillAmount = skill_1_realtime / skill_1_time;
        } else {
            skill_1_cooltime.fillAmount = 0f;
        }

        if (!ready_skill_2) {
            if (skill_2_realtime <= skill_2_time) {
                skill_2_realtime += Time.deltaTime;
            } else {
                ready_skill_2 = true;
            }
            skill_2_cooltime.fillAmount = skill_2_realtime / skill_2_time;
        } else {
            skill_2_cooltime.fillAmount = 0f;
        }

        if (!ready_skill_3) {
            if (skill_3_realtime <= skill_3_time) {
                skill_3_realtime += Time.deltaTime;
            } else {
                ready_skill_3 = true;
            }
            skill_3_cooltime.fillAmount = skill_3_realtime / skill_3_time;
        } else {
            skill_3_cooltime.fillAmount = 0f;
        }
    }

    public IEnumerator Skill_1()
    {
        if (heal_count > 0 && ready_heal) {
            heal_count -= 1;
            heal_realtime = 0f;
            ready_heal = false;
            player_sound.SKill_1_Sound_Play();
            animator.Play("heal");
            skill_animator.Play("skill_1");
            if (banner.transform.GetChild(1).name == "Change_Image") {
                Image image = banner.transform.GetChild(1).gameObject.GetComponent<Image>();
                image.sprite = skill_1;
            }
            if (banner.transform.GetChild(4).name == "Change_Text") {
                TextMeshProUGUI text = banner.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>();
                text.text = "회복 발동!";
            }
            StartCoroutine(Move_Lock(1.2f));
            StartCoroutine(Create_Effect(heal_effect, 0.3f, 2.0f));
            textmesh.text = heal_count.ToString();
            if (player_movement.health <= 5.0f) {
                player_movement.health += 5f;
            }else {
                player_movement.health = 10f;
            }
            health_controller.Check_Health(player_movement.health);
        }
        yield return null;
    }

    public void change_momo()
    {
        textmesh.text = heal_count.ToString();
    }

    public IEnumerator Skill_2()
    {
        if (!player_movement.is_skill && ready_skill_1 && player_data.playerDataTable.is_stone_1) {
            StartCoroutine(Is_Buff());
            animator.Play("skill_2");
            //Debug.Log("2번 스킬: 버프");
            skill_1_realtime = 0f;
            ready_skill_1 = false;
            StartCoroutine(Move_Lock(1.2f));
            StartCoroutine(Create_Effect(buff_effect_1, 0.0f, 1.0f));
            StartCoroutine(Create_Particle(buff_effect_2, 1.0f, 20.0f));
            if (player_movement.health <= 9.0f) {
                player_movement.health += 1f;
            } else {
                player_movement.health = 10f;
            }
            health_controller.Check_Health(player_movement.health);

            skill_animator.Play("skill_1");
            if (banner.transform.GetChild(1).name == "Change_Image") {
                Image image = banner.transform.GetChild(1).gameObject.GetComponent<Image>();
                image.sprite = skill_2;
            }
            if (banner.transform.GetChild(4).name == "Change_Text") {
                TextMeshProUGUI text = banner.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>();
                text.text = "버프 발동!";
            }

            player_sound.SKill_2_Sound_Play();
        }
        yield return null;
    }

    IEnumerator Is_Buff()
    {
        if (!is_buff) {
            is_buff = true;
            yield return new WaitForSeconds(20.0f);
            is_buff = false;
        }
    }

    public IEnumerator Skill_3()
    {
        if (!player_movement.is_skill && ready_skill_2 && player_data.playerDataTable.is_stone_3) 
        {
            //Debug.Log("3번 스킬: 작은스킬");
            skill_animator.Play("skill_1");
            animator.Play("skill_1");
            skill_2_realtime = 0f;
            ready_skill_2 = false;
            if (banner.transform.GetChild(1).name == "Change_Image") {
                Image image = banner.transform.GetChild(1).gameObject.GetComponent<Image>();
                image.sprite = skill_4;
            }
            if (banner.transform.GetChild(4).name == "Change_Text") {
                TextMeshProUGUI text = banner.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>();
                text.text = "스킬 발동!";
            }
            player_sound.SKill_3_Sound_Play();
            player_movement.is_skill = true;
            player_movement.skill_object_3.SetActive(true);
            yield return new WaitForSeconds(1.2f);
            player_movement.is_skill = false;
            player_movement.skill_object_3.SetActive(false);
        }
    }

    public IEnumerator Skill_4()
    {
        if (!player_movement.is_skill && ready_skill_3 && player_data.playerDataTable.is_stone_2) {
            skill_3_realtime = 0f;
            ready_skill_3 = false;
            StartCoroutine(Move_Lock(0.8f));
            skill_animator.Play("skill_1");
            if (banner.transform.GetChild(1).name == "Change_Image") {
                Image image = banner.transform.GetChild(1).gameObject.GetComponent<Image>();
                image.sprite = skill_3;
            }
            if (banner.transform.GetChild(4).name == "Change_Text") {
                TextMeshProUGUI text = banner.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>();
                text.text = "스킬 발동!";
            }
            player_sound.SKill_4_Sound_Play();
            animator.Play("skill_1");
            player_movement.is_skill = true;
            player_movement.skill_object_4.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            player_movement.is_skill = false;
            player_movement.skill_object_4.SetActive(false);
        }
    }

    IEnumerator Move_Lock(float delay)
    {
        if (!is_heal) {
            is_heal = true;
            yield return new WaitForSeconds(delay);
            is_heal = false;
        }
    }

    IEnumerator Create_Effect(GameObject effect, float delay, float ups)
    {
        yield return new WaitForSeconds(delay);
        GameObject spawn = Instantiate(effect, this.transform.position + new Vector3(0f, ups, 0f), Quaternion.identity);
        Destroy(spawn, 1f);
    }

    IEnumerator Create_Particle(GameObject effect, float delay, float destroy)
    {
        yield return new WaitForSeconds(delay);
        GameObject spawn = Instantiate(effect, this.transform);
        spawn.transform.position += new Vector3(0.0f, 1.0f, 0.0f);
        Destroy(spawn, destroy);
    }
}
