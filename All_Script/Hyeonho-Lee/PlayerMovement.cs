using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float move_speed;
    public float rotate_speed;
    public bool is_move;
    public bool lock_move;

    private float h_axis, v_axis;

    public Vector3 movement_vector;
    private Vector3 camera_forward, hit_position;

    void Start()
    {
        Reset_Status();
    }

    void Update()
    {
        Input_Key();
        Check_Value();
    }

    void FixedUpdate()
    {
        Move(h_axis, v_axis);
    }

    void Reset_Status()
    {
        move_speed = 15.0f;
        rotate_speed = 5.0f;
    }

    void Input_Key()
    {
        h_axis = Input.GetAxisRaw("Horizontal");
        v_axis = Input.GetAxisRaw("Vertical");

        camera_forward = Camera.main.transform.forward;
        camera_forward.y = 0;
        camera_forward = Vector3.Normalize(camera_forward);

        if (Input.GetMouseButton(0)) {
            Mouse_Watch();
        }
    }

    void Check_Value()
    {
        if (h_axis == 0 && v_axis == 0)
            is_move = false;
        else
            is_move = true;
    }

    void Move(float horizontal, float vertical)
    {
        movement_vector = new Vector3(h_axis, 0, v_axis).normalized;
        transform.position += movement_vector * move_speed * Time.deltaTime;
        Move_Turn();
    }

    void Move_Turn()
    {
        if (h_axis == 0 && v_axis == 0)
            return;

        Quaternion newRotation = Quaternion.LookRotation(movement_vector * rotate_speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotate_speed * Time.deltaTime);
    }

    void Mouse_Watch()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        /* 가상의 바닥으로 회전
        Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;
        if (GroupPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointTolook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));
        }*/

        // 물리적 충돌로 회전
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10000f)) {
            hit_position = hit.point;

            if (is_move) {
                transform.LookAt(new Vector3(hit_position.x, transform.position.y, hit_position.z));
            }else {
                Vector3 hit_vector = hit_position - transform.position;
                Quaternion newRotation = Quaternion.LookRotation(hit_vector * rotate_speed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotate_speed * Time.deltaTime * 2.0f);
            }

            //Debug.DrawRay(cameraRay.origin, hit.point, Color.red, 1f);
        }
    }
}
