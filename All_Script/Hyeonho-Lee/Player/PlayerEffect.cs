using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    public GameObject dash_effect;
    public GameObject hit_effect_1;
    public GameObject hit_effect_2;

    private GameObject player;
    private GameObject system;

    void Start()
    {
        player = GameObject.Find("Player");
        system = GameObject.Find("System");
    }

    public IEnumerator Dash_Effect()
    {
        GameObject dash = Instantiate(dash_effect, system.transform);
        dash.transform.position = this.transform.position + new Vector3(0f, 1.5f, 0f);
        dash.transform.rotation = player.transform.rotation;
        Destroy(dash, 1f);
        yield return null;
    }
}
