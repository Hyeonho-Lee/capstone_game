using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    float hAxis;
    float vAxis;

    public Vector3 moveVec;

    Animator anim;
    
    void Walk()
    {

        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * Time.deltaTime;
        Turn();
    }

    void Turn()
    {
        if (hAxis == 0 && vAxis == 0)
            return;
        Quaternion newRotation = Quaternion.LookRotation(moveVec * speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
    }

    void Start()
    {
        //anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Walk();
        //anim.SetBool("isRun", moveVec != Vector3.zero);
    }
}
hi
