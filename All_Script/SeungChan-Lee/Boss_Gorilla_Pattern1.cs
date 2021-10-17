using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Gollira_Pattern1 : MonoBehaviour
{
    public GameObject target;
    public float chase_speed = 10.0f;
    public float stun_time;

    private Transform target_transform;
    private Vector3 dir;

    private bool is_area;
    
    void Chase()
    {
        dir = (target_transform.position - transform.position).normalized;
        transform.position += dir * chase_speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pillar")
        {
            Destroy(other.gameObject);
            Debug.Log("충돌");
            Stun();
            //Boss_Return_State();  //나중에 고릴라 구현 후 상태 변화 사용.
        }
    }

    /*void Boss_Return_State()
    {
        //Stun(time);
        Chase();
    }
*/
    void Stun()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {        
        
    }

    void Start()
    {
        target_transform = target.GetComponent<Transform>();
    }


    void Update()
    {
        Chase();
    }
}
