using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading_Box : MonoBehaviour
{
    public string scene_name;
    public string trigger_scene;

    private World_Admin world_admin;
    private PlayerData player_data;

    void Start()
    {
        world_admin = GameObject.Find("System").GetComponent<World_Admin>();
        player_data = GameObject.Find("System").GetComponent<PlayerData>();
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player")) 
        {
            float dir = Vector3.Angle(transform.forward, other.transform.position - transform.position);
            if (dir < 90f) 
            {
                StartCoroutine(Load_Scene());
                world_admin.Check_Scene_Exit();
            } else 
            {
                StartCoroutine(UnLoad_Scene());
                world_admin.Check_Scene_Enter();
            }
        }
    }

    IEnumerator Load_Scene()
    {
        Scene targetScene = SceneManager.GetSceneByName(scene_name);
        if(!targetScene.isLoaded) 
        {
            AsyncOperation load = SceneManager.LoadSceneAsync(scene_name, LoadSceneMode.Additive);

            while(!load.isDone)
            {
                yield return null;
            }
        }
    }

    IEnumerator UnLoad_Scene()
    {
        Scene targetScene = SceneManager.GetSceneByName(scene_name);
        if (targetScene.isLoaded) {
            Scene current_scene = SceneManager.GetSceneByName(trigger_scene);
            SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("Get_Next"), current_scene);

            if (player_data.playerDataTable.town && !player_data.playerDataTable.first_scene || player_data.playerDataTable.stage_2 && !player_data.playerDataTable.first_scene) {
                player_data.playerDataTable.first_scene = true;

                AsyncOperation unload = SceneManager.UnloadSceneAsync(scene_name);
                AsyncOperation unloads = SceneManager.UnloadSceneAsync("MainPlayRoom");

                while (!unload.isDone && !unloads.isDone) {
                    yield return null;
                }
            }else {
                AsyncOperation unload = SceneManager.UnloadSceneAsync(scene_name);

                while (!unload.isDone) {
                    yield return null;
                }
            }
        }
    }
}
