using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Admin_Wall : MonoBehaviour
{
    public Material object_mat;
    public Color mat_color;

    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        object_mat = renderer.material;
        mat_color = object_mat.color;
    }

    void Update()
    {
        Re_Mat();
    }

    public void Change_Mat()
    {
        mat_color.a = 0.3f;
        object_mat.color = mat_color;
    }

    void Re_Mat()
    {
        mat_color.a = 1.0f;
        object_mat.color = mat_color;
    }
}
