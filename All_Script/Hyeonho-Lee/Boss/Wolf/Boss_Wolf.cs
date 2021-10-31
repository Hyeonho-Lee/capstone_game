using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Wolf : MonoBehaviour
{
    public float wolf_health;

    private bool is_damage;

    public Material damage_mat;
    private Material object_mat;

    private Renderer renderer;
    private Boss_UI_Controller boss_ui;
    private Boss_Wolf_1 patern_1;
    private Boss_Wolf_2 patern_2;
    private Boss_Wolf_3 patern_3;

    void Start()
    {
        renderer = GameObject.Find("Wolf").GetComponent<Renderer>();
        boss_ui = GameObject.Find("System").GetComponent<Boss_UI_Controller>();
        patern_1 = GameObject.Find("Wolf_Patern_1").GetComponent<Boss_Wolf_1>();
        patern_2 = GameObject.Find("Wolf_Patern_2").GetComponent<Boss_Wolf_2>();
        patern_3 = GameObject.Find("Wolf_Patern_3").GetComponent<Boss_Wolf_3>();
        Reset_Status();

        for(int i = 0; i < 100; i++) 
        {
            StartCoroutine(Random_Patern(15f * i));
        }
    }

    void Update()
    {
        if (is_damage) 
        {
            renderer.material = damage_mat;
        } else 
        {
            renderer.material = object_mat;
        }

        if (wolf_health <= 0) 
        {
            boss_ui.is_boss = false;
            StopCoroutine("Random_Patern");
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player_Attack") 
        {
            StartCoroutine(Is_Damage(0.5f));
        }
    }

    void Reset_Status()
    {
        wolf_health = 100.0f;
        object_mat = renderer.material;
    }

    IEnumerator Is_Damage(float delay)
    {
        if (!is_damage) 
        {
            is_damage = true;
            wolf_health -= 1.0f;
            yield return new WaitForSeconds(delay);
            is_damage = false;
        }
    }

    IEnumerator Random_Patern(float delay)
    {
        yield return new WaitForSeconds(delay);
        int random = Random.Range(1, 4);

        if(random == 1) 
        {
            Patern_1();
        }

        if (random == 2) 
        {
            Patern_2();
        }

        if (random == 3)
        {
            Patern_3();
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
}
