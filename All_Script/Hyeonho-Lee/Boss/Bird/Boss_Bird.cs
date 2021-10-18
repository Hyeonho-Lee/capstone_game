using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bird : MonoBehaviour
{
    public float bird_health;

    private bool is_damage;

    public Material damage_mat;
    private Material object_mat;

    private Renderer renderer;
    private Boss_Bird_1 patern_1;
    private Boss_Bird_2 patern_2;
    private Boss_Bird_3 patern_3;
    private Boss_Bird_4 patern_4;

    void Start()
    {
        renderer = GameObject.Find("Bird").GetComponent<Renderer>();
        patern_1 = GameObject.Find("Bird_Patern_1").GetComponent<Boss_Bird_1>();
        patern_2 = GameObject.Find("Bird_Patern_2").GetComponent<Boss_Bird_2>();
        patern_3 = GameObject.Find("Bird_Patern_3").GetComponent<Boss_Bird_3>();
        patern_4 = GameObject.Find("Bird_Patern_4").GetComponent<Boss_Bird_4>();

        Reset_Status();

        for (int i = 0; i < 100; i++) {
            StartCoroutine(Random_Patern(15f * i));
        }
    }

    void Update()
    {
        if (is_damage) {
            renderer.material = damage_mat;
        } else {
            renderer.material = object_mat;
        }

        if (bird_health <= 0) {
            StopCoroutine("Random_Patern");
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player_Attack") {
            StartCoroutine(Is_Damage(0.5f));
        }
    }

    void Reset_Status()
    {
        bird_health = 100.0f;
        object_mat = renderer.material;
    }

    IEnumerator Is_Damage(float delay)
    {
        if (!is_damage) {
            is_damage = true;
            bird_health -= 1.0f;
            yield return new WaitForSeconds(delay);
            is_damage = false;
        }
    }

    IEnumerator Random_Patern(float delay)
    {
        yield return new WaitForSeconds(delay);
        int random = Random.Range(1, 5);

        if (random == 1) {
            Patern_1();
        }

        if (random == 2) {
            Patern_2();
        }

        if (random == 3) {
            Patern_3();
        }

        if (random == 4) {
            Patern_4();
        }
    }

    public void Patern_1()
    {
        patern_1.Attack();
    }

    public void Patern_2()
    {
        patern_2.Attack();
    }

    public void Patern_3()
    {
        patern_3.Attack();
    }

    public void Patern_4()
    {
        patern_4.Attack();
    }
}
