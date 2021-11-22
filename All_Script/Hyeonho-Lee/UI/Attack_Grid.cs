using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack_Grid : MonoBehaviour
{
    public float time;
    public float real_time;

    public bool is_enemy;
    private bool is_done;

    void Start()
    {
        real_time = 0;
        this.transform.localScale = new Vector3(0.01f, 0.01f, 1.0f);
        is_done = false;
    }

    void Update()
    {
        if (!is_done) {
            real_time += Time.deltaTime;

            if (real_time >= time) {
                this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }else {
                this.transform.localScale = new Vector3(real_time / time, real_time / time, 1.0f);
            }

            if (real_time >= time + 0.5f && is_enemy) {
                real_time = 0;
            }
        }
    }

}
