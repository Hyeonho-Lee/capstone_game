using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Loading_Scene : MonoBehaviour
{
    public Image loading;

    private float time;

    void Start()
    {
        time = 0;
        StartCoroutine(Now_Loading());
    }

    IEnumerator Now_Loading()
    {
        AsyncOperation scene = SceneManager.LoadSceneAsync("MainPlayRoom");
        scene.allowSceneActivation = false;

        while (!scene.isDone) {
            yield return null;
            time = +Time.time;

            if(scene.progress < 0.9f) {
                if (loading != null) {
                    loading.fillAmount = time / 5f;
                    if(loading.fillAmount >= scene.progress) {
                        time = 0;
                    }
                }
            }else {
                if(loading != null) {
                    loading.fillAmount = Mathf.Lerp(loading.fillAmount, 1f, time);
                    if(loading.fillAmount >= 1.0f) {
                        scene.allowSceneActivation = true;
                        yield break;
                    }
                }
            }
        }
    }
}
