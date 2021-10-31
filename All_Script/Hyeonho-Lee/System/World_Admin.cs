using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class World
{
    public string world_name;
    public bool is_world_enter;
}

public class World_Admin : MonoBehaviour
{
    public World[] world = new World[12];

    private string scene_name;

    void Start()
    {
        Check_Next();
        Input_World();
    }

    void Update()
    {
        Check_Stage();
    }

    void Check_Next()
    {
        scene_name = SceneManager.GetActiveScene().name;

        if (GameObject.FindGameObjectsWithTag("Get_Next").Length != 1) 
        {
            Destroy(GameObject.FindGameObjectsWithTag("Get_Next")[1]);
        }
    }

    void Check_Stage()
    {
        for (int i = 0; i < world.Length; i++) 
        {
            if (scene_name == world[i].world_name) 
            {
                world[i].is_world_enter = true;
            } else 
            {
                world[i].is_world_enter = false;
            }
        }
    }

    void Input_World()
    {
        world[0].world_name = "MainPlayRoom";
        world[1].world_name = "Play_Town";
        world[2].world_name = "Play_Stage_1";
        world[3].world_name = "Play_Stage_2";
        world[4].world_name = "Play_Stage_3";
        world[5].world_name = "Play_Stage_4";
        world[6].world_name = "Play_Stage_5";
        world[7].world_name = "Play_Boss_Wolf";
        world[8].world_name = "Play_Boss_Bird";
        world[9].world_name = "Play_Boss_Monkey";
        world[10].world_name = "Play_Boss_End";
    }

    public void Check_Scene_Enter()
    {
        scene_name = SceneManager.GetActiveScene().name;
        Debug.Log(scene_name + "진입");
    }

    public void Check_Scene_Exit()
    {
        scene_name = SceneManager.GetActiveScene().name;
        Debug.Log(scene_name + "나감");
    }
}
