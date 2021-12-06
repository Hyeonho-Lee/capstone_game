using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy_0 : MonoBehaviour
{
    public string enemy_name;
    public float enemy_health;
    public float start_health;
    private float value;
    public float distance;
    public float font_size;

    public int item_index;
    public float item_percent;

    private bool is_damage;
    private bool is_die;

    public GameObject die_effect;

    public AudioClip enemy_hit_sound;
    private GameObject player;
    private GameObject system;
    private Slider health_ui;

    private Item_Table item_table;
    private TextMeshPro textmeshs;
    private CapsuleCollider collider;
    private AudioSource audio;

    void Start()
    {
        player = GameObject.Find("Player");
        system = GameObject.Find("System");
        item_table = system.GetComponent<Item_Table>();
        collider = GetComponent<CapsuleCollider>();
        audio = GetComponent<AudioSource>();
        Reset_Status();
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= 22) {
            if (distance >= 5) {
                font_size = distance / 5 + 3;
            }else {
                font_size = 4;
            }
        }else {
            font_size = 7;
        }

        if (health_ui != null) {
            health_ui.value = enemy_health / start_health;
        }

        if (enemy_health <= 0) {
            if (!is_die) {
                StartCoroutine(Is_Die());
            }
        }

        if(is_die) {
            if (value <= 1f) {
                value += Time.deltaTime * 0.2f;
            }
        }

        if (transform.GetChild(0) != null) {
            if (transform.GetChild(0).name == "3D Canvas") {
                GameObject canvas = transform.GetChild(0).gameObject;
                if (canvas.transform.GetChild(0).name == "Name") {
                    GameObject name = canvas.transform.GetChild(0).gameObject;
                    name.transform.LookAt(GameObject.Find("Main Camera").transform);
                    textmeshs = name.GetComponent<TextMeshPro>();
                    textmeshs.text = enemy_name;
                    textmeshs.fontSize = font_size;
                }
                if (canvas.transform.GetChild(1).name == "Slider") {
                    GameObject result = canvas.transform.GetChild(1).gameObject;
                    result.transform.LookAt(GameObject.Find("Main Camera").transform);
                    health_ui = result.GetComponent<Slider>();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player_Attack" && !is_die) {
            StartCoroutine(Is_Damage(0.3f));
        }
    }

    void Reset_Status()
    {
        //start_health = 30.0f;
        enemy_health = start_health;
    }

    IEnumerator Is_Damage(float delay)
    {
        if (!is_damage) {
            is_damage = true;
            enemy_health -= 1.0f;
            audio.PlayOneShot(enemy_hit_sound);
            yield return new WaitForSeconds(delay);
            is_damage = false;
        }
    }

    IEnumerator Is_Die()
    {
        is_die = true;
        value = 0;
        Drop_Item(item_index - 1, item_percent);
        if (die_effect != null) {
            GameObject die_effects = Instantiate(die_effect);
            die_effects.transform.position = this.transform.position;
            Destroy(die_effects, 2.0f);
        }
        Destroy(this.gameObject, 0.2f);
        yield return null;
    }

    void Drop_Item(int index, float percent)
    {
        float random_value = Random.Range(0.0f, 1.0f);
        if (random_value <= percent && percent != 0.0f) {
            GameObject items = Instantiate(item_table.item_prefab[index]);
            items.transform.position = this.transform.position;
            //Debug.Log(item_table.itemtable[index].name);
            //Debug.Log("µå¶ø!");
        }
    }
}
