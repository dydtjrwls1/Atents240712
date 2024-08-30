using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // 카메라가 따라다닐 대상의 트랜스폼
    public Transform target;

    // 카메라 위치가 보간되는 정도
    public float smooth = 3.0f;

    // 대상과 카메라의 간격
    Vector3 offset;

    // target 과 camera 사이의 실제 거리 (= offset 의 거리)
    float length;

    void Start()
    {
        if(target == null)
        {
            Player player = GameManager.Instance.Player;
            target = player.transform.GetChild(8);
        }

        offset = transform.position - target.position; // 타겟에서 카메라로 가는 방향 벡터
        length = offset.magnitude;
    }

    private void FixedUpdate()
    {
        UpdateCamera();

        Ray ray = new Ray(target.position, transform.position - target.position); // 카메라 root에서 카메라 방향으로 나가는 ray
        if(Physics.Raycast(ray, out RaycastHit hitInfo, length))
        {
            transform.position = hitInfo.point; // 충돌한 위치로 즉시 옮기기
        }
    }

    // void LateUpdate() { } 물리와 일반 업데이트 차이 때문에 떨리는 현상이 발생.

    void UpdateCamera()
    {
        transform.position = Vector3.Slerp(
            transform.position,
            target.position + Quaternion.LookRotation(target.forward) * offset,
            Time.fixedDeltaTime * smooth);
        transform.LookAt(target);
    }
}
