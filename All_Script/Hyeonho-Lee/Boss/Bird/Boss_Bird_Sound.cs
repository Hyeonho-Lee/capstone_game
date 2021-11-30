using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bird_Sound : MonoBehaviour
{
    private AudioSource audio;

    public AudioClip skill_0_sound;
    public AudioClip skill_1_sound;
    public AudioClip skill_1_1_sound;
    public AudioClip skill_2_sound;
    public AudioClip skill_3_sound;
    public AudioClip skill_4_sound;
    public AudioClip hit_sound;

    void Start()
    {
        audio = this.GetComponent<AudioSource>();
    }

    public void Sound_Play(AudioClip sound)
    {
        audio.PlayOneShot(sound);
    }
}
