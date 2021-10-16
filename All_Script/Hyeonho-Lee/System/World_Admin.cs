using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_Admin : MonoBehaviour
{
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Get_Next").Length != 1) 
        {
            Destroy(GameObject.FindGameObjectsWithTag("Get_Next")[1]);
        }
    }
}
