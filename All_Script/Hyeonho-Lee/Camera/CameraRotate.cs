using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float rotate_speed;
    private float rotate_y;

    private Vector3 first_value;

    void Start()
    {
        first_value = this.transform.rotation.eulerAngles;
    }

    void Update()
    {
        rotate_y += rotate_speed * Time.deltaTime;
        this.transform.rotation = Quaternion.Euler(first_value.x, first_value.y + rotate_y, first_value.z);
    }
}
