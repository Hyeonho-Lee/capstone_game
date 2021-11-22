using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float value;
    private bool is_shake;

    private Transform camera;
    private Vector3 original_pos;

    void Start()
    {
        camera = GameObject.Find("Main Camera").transform;
        value = 0.12f;
    }

    void Update()
    {
        original_pos = camera.transform.position;
        if (is_shake) {
            camera.position = original_pos + Random.insideUnitSphere * value;
        }else {
            camera.position = original_pos;
        }
    }

    public IEnumerator Shake(float time)
    {
        if (!is_shake) {
            is_shake = true;
            yield return new WaitForSeconds(time);
            is_shake = false;
        }
    }
}
