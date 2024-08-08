using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissile : EnemyBase
{
    // HP 는 1 이고 터트렸을 때 점수는 0점

    // 생성되자 마자 플레이어를 추적함(플레이어 방향으로 이동)
    // 자신의 트리거 안에 플레이어가 들어오면 플레이어 추적 중지
    // 추적 정도를 설정할 수 있는 변수 만들기
    Vector3 direction;

    Player player;

    private void Awake()
    {
        player = GameManager.Instance.Player;
    }

    protected override void OnMoveUpdate(float deltaTime)
    {
        base.OnMoveUpdate(deltaTime);
        float bulletAngle = Vector3.Angle(Vector3.right, transform.position);
        float playerAngle = Vector3.Angle(Vector3.right, player.transform.position);
        float angle = Mathf.Lerp(0.1f, bulletAngle, playerAngle);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
