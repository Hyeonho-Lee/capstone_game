using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private AudioSource audio;

    public AudioClip skill_1_sound; // 스킬1 사운드 추가 및 Player_Skill 25 스킬1 함수 수정
    public AudioClip skill_2_sound; // 스킬2 사운드 추가 및 Player_Skill 34 스킬2 함수 수정
    public AudioClip skill_3_sound; // 스킬3 사운드 추가 및 Player_Skill 45 스킬3 함수 수정
    public AudioClip skill_4_sound; // 스킬4 사운드 추가 예정 Player_Skill 66
    public AudioClip shield_sound; // 쉴드 사운드 추가 및 PlayerMovement 357 디펜스 함수 수정
    public AudioClip pick_sound; // 아이템 줍는 소리 추가 및 Player_Interaction 159 아이템 줍는 함수 수정
    public AudioClip hit_sound; // 피격 사운드 추가 및 PlayerMovement 347 피격 함수 수정
    public AudioClip dash_sound;
    public AudioClip attack_1_sound; // 공격1 소리 추가
    public AudioClip attack_2_sound; // 공격2 소리 추가
    public AudioClip attack_3_sound; // 공격3 소리 추가


    void Start()
    {
        audio = this.GetComponent<AudioSource>();
    }

    public void SKill_1_Sound_Play()
    {
        Check_Error(skill_1_sound);
    }

    public void SKill_2_Sound_Play()
    {
        Check_Error(skill_2_sound);
    }

    public void SKill_3_Sound_Play()
    {
        Check_Error(skill_3_sound);
    }

    public void SKill_4_Sound_Play()
    {
        Check_Error(skill_4_sound);
    }

    public void Shield_Sound_Play()
    {
        Check_Error(shield_sound);
    }

    public void Dash_Sound_Play()
    {
        Check_Error(dash_sound);
    }

    public void Pick_Sound_Play()
    {
        Check_Error(pick_sound);
    }

    public void Player_Hit_Sound_Play()
    {
        Check_Error(hit_sound);
    }

    public void Player_Attack_1_Sound_Play()
    {
        Check_Error(attack_1_sound);
    }

    public void Player_Attack_2_Sound_Play()
    {
        Check_Error(attack_2_sound);
    }

    public void Player_Attack_3_Sound_Play()
    {
        Check_Error(attack_3_sound);
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
