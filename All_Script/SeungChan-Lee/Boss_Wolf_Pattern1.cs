using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Wolf_Test : MonoBehaviour
{
    public float dash_speed;
    public float dash_wait_time;
    public int count_dash = 3;

    private Vector3 dir;

    private Rigidbody rigidbody;
    public GameObject target;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        target = GameObject.Find("Player");
        StartCoroutine(Dash(dash_wait_time));
    }

    private Vector3 Wolf_Heading_Vec()
    {
        Vector3 heading = target.transform.position - transform.position;
        var distance = heading.magnitude;
        dir = heading / distance;

        Debug.Log(dir);

        return dir;
    }

    private void Add_Force(Vector3 dir, float speed)
    {
        rigidbody.AddForce(dir * speed, ForceMode.Impulse);
    }
    
    IEnumerator Dash(float delay)
    {
        yield return new WaitForSeconds(delay);
        Add_Force(Wolf_Heading_Vec(), dash_speed);
        yield return new WaitForSeconds(delay);
        Add_Force(Wolf_Heading_Vec(), dash_speed);
        yield return new WaitForSeconds(delay);
        Add_Force(Wolf_Heading_Vec(), dash_speed);
    }

    void Update()
    {
        
    }
}
