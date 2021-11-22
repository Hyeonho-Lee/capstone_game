using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Wolf_Sound : MonoBehaviour
{
    private AudioSource audio;

    public AudioClip skill_0_sound;
    public AudioClip skill_1_1_sound;
    public AudioClip skill_1_2_sound;
    public AudioClip skill_1_3_sound;
    public AudioClip skill_2_1_sound;
    public AudioClip skill_2_2_sound;
    public AudioClip skill_2_3_sound;
    public AudioClip skill_3_sound;
    public AudioClip hit_sound;

    void Start()
    {
        audio = this.GetComponent<AudioSource>();
    }

    public void SKill_0_Sound_Play()
    {
        Check_Error(skill_0_sound);
    }

    public void SKill_1_1_Sound_Play()
    {
        Check_Error(skill_1_1_sound);
    }

    public void SKill_1_2_Sound_Play()
    {
        Check_Error(skill_1_2_sound);
    }

    public void SKill_1_3_Sound_Play()
    {
        Check_Error(skill_1_3_sound);
    }

    public void SKill_2_1_Sound_Play()
    {
        Check_Error(skill_2_1_sound);
    }

    public void SKill_2_2_Sound_Play()
    {
        Check_Error(skill_2_2_sound);
    }

    public void SKill_2_3_Sound_Play()
    {
        Check_Error(skill_2_3_sound);
    }

    public void SKill_3_Sound_Play()
    {
        Check_Error(skill_3_sound);
    }

    public void Hit_Sound_Play()
    {
        Check_Error(hit_sound);
    }

    public void Sound_Down()
    {
        audio.volume = 0.45f;
    }

    public void Sound_Up()
    {
        audio.volume = 1f;
    }

    void Check_Error(AudioClip sound)
    {
        if (sound != null) {
            audio.PlayOneShot(sound);
        }
    }
}
