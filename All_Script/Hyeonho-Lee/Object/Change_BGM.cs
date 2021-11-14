using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_BGM : MonoBehaviour
{
    public int index;

    // Inspector 영역에 표시할 배경음악 이름
    private string bgmName;
    public AudioClip play_bgm;
    
    private GameObject CamObject;
    private Camera_BGM_Controller bgm_controller;
    private AudioSource audio;

    void Start()
    {
        CamObject = GameObject.Find("Main Camera");
        bgm_controller = CamObject.GetComponent<Camera_BGM_Controller>();
        audio = CamObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            bgmName = bgm_controller.BGMList[index].name;
            play_bgm = audio.clip;
            if(play_bgm != bgm_controller.BGMList[index].audio) {
                CamObject.GetComponent<Camera_BGM_Controller>().PlayBGM(bgmName);
            }
        }
    }
}
