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

    void Start()
    {
        if(target == null)
        {
            Player player = GameManager.Instance.Player;
            target = player.transform.GetChild(8);
        }

        offset = transform.position - target.position; // 타겟에서 카메라로 가는 방향 벡터
    }

    private void FixedUpdate()
    {
        // 대상 위치에서 회전된 offset 만큼 떨어진다 ( 회전 정도는 타겟의 y축 회전만큼 )
        transform.position = Vector3.Slerp(
            transform.position,
            target.position + Quaternion.LookRotation(target.forward) * offset,
            Time.fixedDeltaTime * smooth);

        transform.LookAt(target);
    }

    // void LateUpdate() { } 물리와 일반 업데이트 차이 때문에 떨리는 현상이 발생.
}
