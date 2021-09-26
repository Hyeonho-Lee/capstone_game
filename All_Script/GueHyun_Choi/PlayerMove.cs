using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    float moveSpeed = 4f;
    Vector3 forward, right;

    Vector3 targetPos;

    void Start() {
        // forward는 방향 벡터이며 앞을 나타낸다. 
        // Vector3.forward는 월드 좌표계 기준 앞, transform.forward는 로컬 좌표계 기준 앞이다.
        forward = Camera.main.transform.forward; 
        forward.y = 0; // 원래 forward는 아래를 봄, 바닥을 기준으로 변경
        forward = Vector3.Normalize(forward);

        // 좌우 움직임 구현을 위해 좌방향 벡터 계산
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    void Update() 
    {
        if (Input.anyKey)
        {
            Move();
        }
        
        if (Input.GetMouseButton(1)) // 마우스 클릭 시 실행
        {
            MouseWatching();
        }
    }

    void MouseWatching()
    {
        

        // ScreenPointToRay : 스크린 좌표를 인수로 주는 것으로 스크린 좌표에 해당하는 3차원 좌표로 Ray 생성
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Plane : 하나의 면을 뜻하며 아래의 경우 면에 수직인 벡터에 지나는 한 점을 이용해 면을 생성한다. 
        Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        /*if (GroupPlane.Raycast(cameraRay, out rayLength))
        // Palne.Raycast : 입력받은 ray에 충돌 시 ray의 origin 부터 충돌한 점까지의 거리를 rayLength에 넣어주고 true 반환
        {
            // 충돌 지점까지의 거리를 통해 충돌 지점을 전달해 준다. 
            Vector3 pointTolook = cameraRay.GetPoint(rayLength);

            Debug.Log(cameraRay.GetPoint(rayLength)); // 확인

            // forward 백터가 해당 방향으로 바라보도록 만들어 준다.
            transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));
        }*/

        

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 3차원 좌표로 Ray 생성
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
        // Input의 GetAxis에 설정되있는 버튼들을 입력 시 움직이게 만들어 준다.
        // 쿼터 뷰 시점을 위해 카메라 기준으로 설정되 있는 right, forward 값을 넣어주었다. 
        Vector3 RightMovement = right * moveSpeed * Time.smoothDeltaTime * Input.GetAxis("Horizontal");
        Vector3 ForwardMovement = forward * moveSpeed * Time.smoothDeltaTime * Input.GetAxis("Vertical");
        Vector3 FinalMovement = ForwardMovement + RightMovement;

        // 움직이는 방향을 바라보기위한 값
        Vector3 direction = Vector3.Normalize(FinalMovement);

        if (FinalMovement != Vector3.zero)
        {
            transform.position += FinalMovement;

            if (!Input.GetMouseButton(1)) // 클릭 하지 않으면 이동 방향을 바라봄
            {
                transform.forward = direction;
            }

        }
    }

}
