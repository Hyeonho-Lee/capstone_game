using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_AreaLock : MonoBehaviour
{
    public GameObject area_lock;
    private GameObject boss;

    void Update()
    {
        if (GameObject.Find("Wolf_Patern") != true) {
            area_lock.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            area_lock.SetActive(true);
        }
    }
}
