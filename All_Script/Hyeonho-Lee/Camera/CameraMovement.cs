using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float camera_angle;
    public Vector3 offset;

    public float distance;
    public Vector3 dir;

    private GameObject player;
    private Renderer object_renderer;
    private Material object_mat;
    private Admin_Wall admin_wall;

    void Start()
    {
        Reset_Status();
        Find_GameObject("Player");
    }

    void Update()
    {
        Find_Wall();
    }

    void FixedUpdate()
    {
        Follow_Camera();
    }

    void Reset_Status()
    {
        camera_angle = 45.0f;
        offset.y = 16.6f;
        offset.z = -18.1f;
    }

    void Find_GameObject(string name)
    {
        if (GameObject.FindWithTag(name)) {
            player = GameObject.Find(name);
            //Debug.Log("Console: " + name + "를 발견하였습니다.");
        }else {
            player = new GameObject("default");
            //Debug.Log("Console: " + name + "가 없습니다.");
        }
    }

    void Follow_Camera()
    {
        transform.position = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, player.transform.position.z + offset.z);
        transform.rotation = Quaternion.Euler(camera_angle, 0.0f, 0.0f);
    }

    void Find_Wall()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        dir = (player.transform.position - transform.position).normalized;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, distance)) 
        {
            if(hit.transform.tag == "Wall" || hit.transform.tag == "Pillar") 
            {
                admin_wall = hit.collider.gameObject.GetComponent<Admin_Wall>();
                StartCoroutine(admin_wall.Change_Material(3.0f));
            }
        }

        Debug.DrawLine(transform.position, player.transform.position, Color.red);
    }
}
