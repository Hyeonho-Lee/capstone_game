using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_Scene : MonoBehaviour
{
    private string scene_name;
    private string trigger_scene;

    private GameObject player;
    private World_Admin world_admin;
    private PlayerData player_data;

    void Start()
    {
        player = GameObject.Find("Player");
        world_admin = GameObject.Find("System").GetComponent<World_Admin>();
        player_data = GameObject.Find("System").GetComponent<PlayerData>();
        player_data.PlayerDataLoad();

        if (player_data.playerDataTable.town) {
            scene_name = "Play_Town";
            trigger_scene = "MainPlayRoom";
            StartCoroutine(Load_Scenes());
            world_admin.Check_Scene_Exit();

            player.transform.position = new Vector3(player_data.playerDataTable.player_position_x, 1.0f, player_data.playerDataTable.player_position_z);

            scene_name = "MainPlayRoom";
            trigger_scene = "Play_Town";
            StartCoroutine(UnLoad_Scenes());
            world_admin.Check_Scene_Enter();
        }
        
        if (player_data.playerDataTable.stage_2) {
            scene_name = "Play_Stage_2";
            trigger_scene = "MainPlayRoom";
            StartCoroutine(Load_Scenes());
            world_admin.Check_Scene_Exit();

            player.transform.position = new Vector3(player_data.playerDataTable.player_position_x, 1.0f, player_data.playerDataTable.player_position_z);

            scene_name = "MainPlayRoom";
            trigger_scene = "Play_Stage_2";
            StartCoroutine(UnLoad_Scenes());
            world_admin.Check_Scene_Enter();
        }
        //Debug.Log(player_data.playerDataTable.town);
    }

    IEnumerator Load_Scenes()
    {
        Scene targetScene = SceneManager.GetSceneByName(scene_name);
        if (!targetScene.isLoaded) {
            AsyncOperation load = SceneManager.LoadSceneAsync(scene_name, LoadSceneMode.Additive);

            while (!load.isDone) {
                yield return null;
            }
        }
    }

    IEnumerator UnLoad_Scenes()
    {
        Scene targetScene = SceneManager.GetSceneByName(scene_name);
        if (targetScene.isLoaded) {
            Scene current_scene = SceneManager.GetSceneByName(trigger_scene);
            SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("Get_Next"), current_scene);

            AsyncOperation unload = SceneManager.UnloadSceneAsync(scene_name);

            while (!unload.isDone) {
                yield return null;
            }
        }
    }
}
