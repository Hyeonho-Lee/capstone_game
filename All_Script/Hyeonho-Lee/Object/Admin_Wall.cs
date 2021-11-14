using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Admin_Wall : MonoBehaviour
{
    public bool is_wall;
    public Material change_mat;
    private Material object_mat;
    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        object_mat = renderer.material;
    }

    void Update()
    {
        if(is_wall) {
            renderer.material = change_mat;
        }else {
            renderer.material = object_mat;
        }
    }

    public IEnumerator Change_Material(float delay)
    {
        if (!is_wall) {
            is_wall = true;
            yield return new WaitForSeconds(delay);
            is_wall = false;
        }
    }
}
