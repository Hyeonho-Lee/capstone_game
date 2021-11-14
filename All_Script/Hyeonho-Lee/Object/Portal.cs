using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform teleport_position;
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            if (teleport_position != null) {
                player.transform.position = teleport_position.position + new Vector3(0f, 2f, 0f);
            }else {
                Debug.Log("텔포 위치가 없습니다.");
            }
        }
    }
}
