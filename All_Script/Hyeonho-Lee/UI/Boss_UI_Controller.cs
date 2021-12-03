using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_UI_Controller : MonoBehaviour
{
    public GameObject boss_ui;
    public Slider boss_health_bar;
    public Text boss_text_name;

    private string boss_name;
    public bool is_boss;

    private World_Admin world_admin;
    private Boss_Wolf boss_wolf;
    private Boss_Bird boss_bird;
    private Boss_Monkey boss_monkey;

    void Start()
    {
        world_admin = GameObject.Find("System").GetComponent<World_Admin>();
    }

    void Update()
    {
        Check_Boss();
        Active_Boss_UI();
    }

    void Check_Boss()
    {
        if (GameObject.Find("Wolf_Patern") != null) {
            boss_wolf = GameObject.Find("Wolf_Patern").GetComponent<Boss_Wolf>();
            is_boss = true;
            boss_name = "È­¿°À» ÀÌ²ô´Â ´Á´ë";
            boss_text_name.text = boss_name;
            if (is_boss) {
                boss_health_bar.value = boss_wolf.wolf_health / 150;
            } else {
                boss_health_bar.value = 1;
            }
        } else if (GameObject.Find("Bird_Patern") != null) {
            boss_bird = GameObject.Find("Bird_Patern").GetComponent<Boss_Bird>();
            is_boss = true;
            boss_name = "³¯Ä«·Î¿î ¹ø°³¸¦ µÎ¸¥ ÇÐ";
            boss_text_name.text = boss_name;
            if (is_boss) {
                boss_health_bar.value = boss_bird.bird_health / 150;
            } else {
                boss_health_bar.value = 1;
            }
        } else if (GameObject.Find("Monkey_Patern") != null) {
            boss_monkey = GameObject.Find("Monkey_Patern").GetComponent<Boss_Monkey>();
            is_boss = true;
            boss_name = "µ¹·Î ¸öÀ» µÑ·¯½Ñ ¿ø¼þÀÌ";
            boss_text_name.text = boss_name;
            if (is_boss) {
                boss_health_bar.value = boss_monkey.monkey_health / 200;
            } else {
                boss_health_bar.value = 1;
            }
        } else {
            is_boss = false;
        }
    }

    void Active_Boss_UI()
    {
        if (is_boss)
        {
            boss_ui.SetActive(true);
        } else 
        {
            boss_ui.SetActive(false);
        }
    }
}
