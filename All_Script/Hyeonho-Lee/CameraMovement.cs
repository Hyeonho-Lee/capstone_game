using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float camera_angle;
    public Vector3 offset;
    private GameObject player;

    void Start()
    {
        Reset_Status();
        Find_GameObject("Player");
    }

    void FixedUpdate()
    {
        Follow_Camera();
    }

    void Reset_Status()
    {
        camera_angle = 45.0f;
        offset.y = 12.0f;
        offset.z = -12.0f;
    }

    void Find_GameObject(string name)
    {
        if (GameObject.FindWithTag(name)) {
            player = GameObject.Find(name);
            Debug.Log("Console: " + name + "를 발견하였습니다.");
        }else {
            player = new GameObject("default");
            Debug.Log("Console: " + name + "가 없습니다.");
        }
    }

    void Follow_Camera()
    {
        transform.position = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, player.transform.position.z + offset.z);
        transform.rotation = Quaternion.Euler(camera_angle, 0.0f, 0.0f);
    }
}
