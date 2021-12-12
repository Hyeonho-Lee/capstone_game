using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_End : MonoBehaviour
{
    public GameObject Ends;
    public GameObject shop_0;

    private World_Admin world_admin;

    void Start()
    {
        world_admin = GameObject.Find("System").GetComponent<World_Admin>();
    }

    void Update()
    {
        if (world_admin.is_end || world_admin.is_ends) {
            Ends.SetActive(true);
            shop_0.SetActive(false);
        }
    }
}
