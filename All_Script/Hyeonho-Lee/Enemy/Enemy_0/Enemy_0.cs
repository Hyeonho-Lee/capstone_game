using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_0 : MonoBehaviour
{
    public float enemy_health = 30f;
    private float value;

    private bool is_damage;
    private bool is_die;

    public Material damage_mat;
    public Material die_mat;
    public AudioClip enemy_hit_sound;
    private Material object_mat;

    private Renderer renderer;
    private BoxCollider collider;
    private AudioSource audio;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        collider = GetComponent<BoxCollider>();
        audio = GetComponent<AudioSource>();
        Reset_Status();
    }

    void Update()
    {
        if (is_damage) {
            renderer.material = damage_mat;
        } else {
            renderer.material = object_mat;
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

            renderer = GetComponent<Renderer>();
            renderer.material = die_mat;
            renderer.material.SetFloat("_threshold", value);
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
        //enemy_health = 30.0f;
        object_mat = renderer.material;
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
        collider.enabled = false;
        Destroy(this.gameObject, 2.5f);
        yield return null;
    }
}
