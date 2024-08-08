using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissile : EnemyBase
{
    // HP 는 1 이고 터트렸을 때 점수는 0점

    // 생성되자 마자 플레이어를 추적함(플레이어 방향으로 이동)
    // 자신의 트리거 안에 플레이어가 들어오면 플레이어 추적 중지
    // 추적 정도를 설정할 수 있는 변수 만들기

    [Header("추적 미사일 데이터")]
    // 미사일의 유도 성능 높을수록 빠르게 target 방향으로 회전한다.
    public float guidedPerformance = 1.5f;

    // 추적 대상
    Transform target;

    // 추적 중인지 표시하는 변수, true 면 추적 중
    bool isGuided = true;

    protected override void OnReset()
    {
        base.OnReset();
        target = GameManager.Instance.Player.transform;
        isGuided = true;
    }

    protected override void OnMoveUpdate(float deltaTime)
    {
        base.OnMoveUpdate(deltaTime);
        if(isGuided)
        {
            Vector2 direction = target.position - transform.position;
            transform.right = Vector3.Slerp(transform.right, -direction, deltaTime * guidedPerformance);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isGuided && collision.CompareTag("Player"))
            isGuided = false;
    }
}
