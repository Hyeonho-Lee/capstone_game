using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading_Box : MonoBehaviour
{
    public string scene_name;
    public string trigger_scene;

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player")) 
        {
            float dir = Vector3.Angle(transform.forward, other.transform.position - transform.position);
            if (dir < 90f) 
            {
                StartCoroutine(Load_Scene());
            }else 
            {
                StartCoroutine(UnLoad_Scene());
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

            AsyncOperation unload = SceneManager.UnloadSceneAsync(scene_name);

            while (!unload.isDone) {
                yield return null;
            }
        }
    }
}
