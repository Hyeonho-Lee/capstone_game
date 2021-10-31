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
        if (world_admin.world[0].is_world_enter) 
        {
            if(GameObject.Find("Wolf_Patern") != null)
            {
                boss_wolf = GameObject.Find("Wolf_Patern").GetComponent<Boss_Wolf>();
                is_boss = true;
                boss_name = "화염을 이끄는 늑대";
                boss_text_name.text = boss_name;
                if (is_boss) {
                    boss_health_bar.value = boss_wolf.wolf_health / 10;
                } else {
                    boss_health_bar.value = 1;
                }
            }
        }

        if (world_admin.world[1].is_world_enter) 
        {
            if (GameObject.Find("Bird_Patern") != null) 
            {
                boss_bird = GameObject.Find("Bird_Patern").GetComponent<Boss_Bird>();
                is_boss = true;
                boss_name = "날카로운 번개를 두른 학";
                boss_text_name.text = boss_name;
                if (is_boss) {
                    boss_health_bar.value = boss_bird.bird_health / 10;
                }else {
                    boss_health_bar.value = 1;
                }
            }
        }

        if (world_admin.world[2].is_world_enter)
        {
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
