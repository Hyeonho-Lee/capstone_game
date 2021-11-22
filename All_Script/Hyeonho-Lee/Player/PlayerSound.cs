using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private AudioSource audio;

    public AudioClip skill_1_sound; // ��ų1 ���� �߰� �� Player_Skill 25 ��ų1 �Լ� ����
    public AudioClip skill_2_sound; // ��ų2 ���� �߰� �� Player_Skill 34 ��ų2 �Լ� ����
    public AudioClip skill_3_sound; // ��ų3 ���� �߰� �� Player_Skill 45 ��ų3 �Լ� ����
    public AudioClip skill_4_sound; // ��ų4 ���� �߰� ���� Player_Skill 66
    public AudioClip shield_sound; // ���� ���� �߰� �� PlayerMovement 357 ���潺 �Լ� ����
    public AudioClip pick_sound; // ������ �ݴ� �Ҹ� �߰� �� Player_Interaction 159 ������ �ݴ� �Լ� ����
    public AudioClip hit_sound; // �ǰ� ���� �߰� �� PlayerMovement 347 �ǰ� �Լ� ����
    public AudioClip dash_sound;
    public AudioClip attack_1_sound; // ����1 �Ҹ� �߰�
    public AudioClip attack_2_sound; // ����2 �Ҹ� �߰�
    public AudioClip attack_3_sound; // ����3 �Ҹ� �߰�


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
