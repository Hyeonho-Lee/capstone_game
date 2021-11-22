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
    public World[] world = new World[10];

    private string scene_name;
    private PlayerData player_data;

    void Start()
    {
        player_data = GameObject.Find("System").GetComponent<PlayerData>();
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

        if (world[5].is_world_enter && player_data.playerDataTable.wolf_boss) {
            if (GameObject.Find("Wolf_Patern") != null) {
                Destroy(GameObject.Find("Wolf_Patern"));
            }
        }

        if (world[1].is_world_enter) {
            player_data.playerDataTable.town = true;
        }else if (world[3].is_world_enter) {
            player_data.playerDataTable.stage_2 = true;
        }else {
            player_data.playerDataTable.town = false;
            player_data.playerDataTable.stage_2 = false;
        }
    }

    void Input_World()
    {
        world[0].world_name = "MainPlayRoom";
        world[1].world_name = "Play_Town";
        world[2].world_name = "Play_Stage_1";
        world[3].world_name = "Play_Stage_2";
        world[4].world_name = "Play_Stage_3";
        world[5].world_name = "Play_Boss_Wolf";
        world[6].world_name = "Play_Boss_Bird";
        world[7].world_name = "Play_Boss_Monkey";
        world[8].world_name = "Play_Boss_End";
        world[9].world_name = "Load";
    }

    public void Check_Scene_Enter()
    {
        scene_name = SceneManager.GetActiveScene().name;
        //Debug.Log(scene_name + "진입");
    }

    public void Check_Scene_Exit()
    {
        scene_name = SceneManager.GetActiveScene().name;
        //Debug.Log(scene_name + "나감");
    }

    public void Load_Scene(int number)
    {
        SceneManager.LoadScene(world[number].world_name);
    }
}
