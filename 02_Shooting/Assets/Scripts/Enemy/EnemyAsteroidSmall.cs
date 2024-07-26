using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAsteroidSmall : EnemyBase
{
    [Header("작은 운석 데이터")]
    // 최소 회전 속도
    public float minRotateSpeed = 30.0f;

    // 최대 회전 속도
    public float maxRotatespeed = 720.0f;

    // 회전 속도 랜덤 분포용 커브
    public AnimationCurve rotateSpeedCurve;

    // 최종 회전 속도
    float rotateSpeed;

    // 이동 방향
    Vector3 direction;

    protected override void OnReset()
    {
        base.OnReset();
        rotateSpeed = minRotateSpeed + rotateSpeedCurve.Evaluate(Random.value) * maxRotatespeed; // 회전속도 랜덤
    }

    protected override void OnMoveUpdate(float deltaTime)
    {
        transform.Translate(deltaTime * speed * direction, Space.World);
        transform.Rotate(0, 0, deltaTime * rotateSpeed);
    }

    public void SetDestination(Vector3 destination)
    {
        direction = (destination - transform.position).normalized;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + direction);
    }
}
