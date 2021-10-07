using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack_Area : MonoBehaviour
{
    public bool is_box;
    public bool is_sphere;
    public bool is_capsule;

    public float spawn_time;
    public float destroy_time;

    public Material attack_area;

    private SphereCollider sphere_collider;
    private BoxCollider box_collider;
    private CapsuleCollider capsule_collider;
    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();

        if (is_box && !is_sphere && !is_capsule)
        {
            Enter_Box();
        }

        if (!is_box && is_sphere && !is_capsule) {
            Enter_Sphere();
        }

        if (!is_box && !is_sphere && is_capsule) {
            Enter_Capsule();
        }
    }

    void Enter_Box()
    {
        box_collider = GetComponent<BoxCollider>();
        box_collider.enabled = false;
        StartCoroutine(Spawn(box_collider, spawn_time));
        Destroy(this.gameObject, spawn_time + destroy_time);
    }

    void Enter_Sphere()
    {
        sphere_collider = GetComponent<SphereCollider>();
        sphere_collider.enabled = false;
        StartCoroutine(Spawn(sphere_collider, spawn_time));
        Destroy(this.gameObject, spawn_time + destroy_time);
    }

    void Enter_Capsule()
    {
        capsule_collider = GetComponent<CapsuleCollider>();
        capsule_collider.enabled = false;
        StartCoroutine(Spawn(capsule_collider, spawn_time));
        Destroy(this.gameObject, spawn_time + destroy_time);
    }

    IEnumerator Spawn(Collider collider, float delay)
    {
        yield return new WaitForSeconds(delay);
        collider.enabled = true;
        renderer.material = attack_area;
    }
}
