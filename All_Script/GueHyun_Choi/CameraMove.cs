using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour { // 20163420 내용 추가!!
    [SerializeField]
    Vector3 offset;
    public GameObject player; // 플레이어 위치를 알기 위해 사용

    void Update() {
        // 카메라의 위치를 플레이어의 위치에 맞춰 계속 변경
        transform.position = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y,
            player.transform.position.z - offset.z);
    }

}
