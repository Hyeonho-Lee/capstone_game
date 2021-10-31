using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skill : MonoBehaviour
{
    private Vector3 dash_vector;

    private GameObject player;
    private PlayerMovement player_movement;
    private Animator animator;
    private Rigidbody rigidbody;

    void Start()
    {
        player = GameObject.Find("Player");
        player_movement = player.GetComponent<PlayerMovement>();
        animator = player.GetComponent<Animator>();
        rigidbody = player.GetComponent<Rigidbody>();
    }

    public IEnumerator Skill_1()
    {
        Debug.Log("1번 스킬: 힐");
        yield return null;
    }

    public IEnumerator Skill_2()
    {
        Debug.Log("2번 스킬: 버프");
        yield return null;
    }

    public IEnumerator Skill_3()
    {
        if (!player_movement.is_skill) 
        {
            Debug.Log("3번 스킬: 작은스킬");
            animator.Play("skill_1");
            player_movement.is_skill = true;
            player_movement.skill_object_3.SetActive(true);
            dash_vector = new Vector3(transform.forward.x, 0, transform.forward.z);
            rigidbody.AddForce(dash_vector * 500f, ForceMode.Impulse);
            yield return new WaitForSeconds(1.0f);
            player_movement.is_skill = false;
            player_movement.skill_object_3.SetActive(false);
        }

        if (player_movement.is_skill) 
        {
            Debug.Log("3번 실행중");
        }
    }

    public IEnumerator Skill_4()
    {
        Debug.Log("4번 스킬: 광역기");
        yield return null;
    }
}
