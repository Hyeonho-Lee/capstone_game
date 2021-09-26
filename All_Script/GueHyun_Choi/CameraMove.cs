using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour { // 20163420 ���� �߰�!!
    [SerializeField]
    Vector3 offset;
    public GameObject player; // �÷��̾� ��ġ�� �˱� ���� ���

    void Update() {
        // ī�޶��� ��ġ�� �÷��̾��� ��ġ�� ���� ��� ����
        transform.position = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y,
            player.transform.position.z - offset.z);
    }

}
