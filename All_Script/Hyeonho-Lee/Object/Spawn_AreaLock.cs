using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_AreaLock : MonoBehaviour
{
    public bool is_wolf;
    public bool is_bird;
    public bool is_monkey;

    public GameObject area_lock;
    private GameObject boss;

    void Update()
    {
        if (is_wolf) {
            if (GameObject.Find("Wolf_Patern") != true) {
                area_lock.SetActive(false);
            }
        }

        if (is_bird) {
            if (GameObject.Find("Bird_Patern") != true) {
                area_lock.SetActive(false);
            }
        }

        if (is_monkey) {
            if (GameObject.Find("Monkey_Patern") != true) {
                area_lock.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            area_lock.SetActive(true);
        }
    }
}
