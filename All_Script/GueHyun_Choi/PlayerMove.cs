using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    float moveSpeed = 4f;
    Vector3 forward, right;

    Vector3 targetPos;

    void Start() {
        // forward�� ���� �����̸� ���� ��Ÿ����. 
        // Vector3.forward�� ���� ��ǥ�� ���� ��, transform.forward�� ���� ��ǥ�� ���� ���̴�.
        forward = Camera.main.transform.forward; 
        forward.y = 0; // ���� forward�� �Ʒ��� ��, �ٴ��� �������� ����
        forward = Vector3.Normalize(forward);

        // �¿� ������ ������ ���� �¹��� ���� ���
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    void Update() 
    {
        if (Input.anyKey)
        {
            Move();
        }
        
        if (Input.GetMouseButton(1)) // ���콺 Ŭ�� �� ����
        {
            MouseWatching();
        }
    }

    void MouseWatching()
    {
        

        // ScreenPointToRay : ��ũ�� ��ǥ�� �μ��� �ִ� ������ ��ũ�� ��ǥ�� �ش��ϴ� 3���� ��ǥ�� Ray ����
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Plane : �ϳ��� ���� ���ϸ� �Ʒ��� ��� �鿡 ������ ���Ϳ� ������ �� ���� �̿��� ���� �����Ѵ�. 
        Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        /*if (GroupPlane.Raycast(cameraRay, out rayLength))
        // Palne.Raycast : �Է¹��� ray�� �浹 �� ray�� origin ���� �浹�� �������� �Ÿ��� rayLength�� �־��ְ� true ��ȯ
        {
            // �浹 ���������� �Ÿ��� ���� �浹 ������ ������ �ش�. 
            Vector3 pointTolook = cameraRay.GetPoint(rayLength);

            Debug.Log(cameraRay.GetPoint(rayLength)); // Ȯ��

            // forward ���Ͱ� �ش� �������� �ٶ󺸵��� ����� �ش�.
            transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));
        }*/

        

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 3���� ��ǥ�� Ray ����
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10000f))
        {
            targetPos = hit.point;
            //Debug.DrawRay(cameraRay.origin, hit.point, Color.red, 1f);

            Debug.Log(targetPos);

            transform.LookAt(new Vector3(targetPos.x, transform.position.y, targetPos.z));
        }

        
    }

    void Move()
    {
        // Input�� GetAxis�� �������ִ� ��ư���� �Է� �� �����̰� ����� �ش�.
        // ���� �� ������ ���� ī�޶� �������� ������ �ִ� right, forward ���� �־��־���. 
        Vector3 RightMovement = right * moveSpeed * Time.smoothDeltaTime * Input.GetAxis("Horizontal");
        Vector3 ForwardMovement = forward * moveSpeed * Time.smoothDeltaTime * Input.GetAxis("Vertical");
        Vector3 FinalMovement = ForwardMovement + RightMovement;

        // �����̴� ������ �ٶ󺸱����� ��
        Vector3 direction = Vector3.Normalize(FinalMovement);

        if (FinalMovement != Vector3.zero)
        {
            transform.position += FinalMovement;

            if (!Input.GetMouseButton(1)) // Ŭ�� ���� ������ �̵� ������ �ٶ�
            {
                transform.forward = direction;
            }

        }
    }

}
