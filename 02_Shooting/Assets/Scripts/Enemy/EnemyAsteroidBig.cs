using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAsteroidBig : EnemyBase
{
    [Header("큰 운석 데이터")]
    // 최소 회전 속도
    public float minRotateSpeed = 30.0f;

    // 최대 회전 속도
    public float maxRotatespeed = 720.0f;

    // 최소 이동 속도
    public float minMoveSpeed = 2.0f;

    // 최대 이동 속도
    public float maxMoveSpeed = 2.0f;

    // 최소 자폭 시간
    public float minExplosiveTime = 3.0f;

    // 최대 자폭 시간
    public float maxExplosiveTime = 5.0f;

    // 회전 속도 랜덤 분포용 커브
    public AnimationCurve rotateSpeedCurve;

    // 최종 회전 속도
    float rotateSpeed;

    // 자폭 까지의 시간
    float explosiveTime;

    // 이동 방향
    Vector3 direction;

    protected override void OnReset()
    {
        base.OnReset();
        explosiveTime = Random.Range(minExplosiveTime, maxExplosiveTime);
        speed = Random.Range(minMoveSpeed, maxMoveSpeed);
        rotateSpeed = minRotateSpeed + rotateSpeedCurve.Evaluate(Random.value) * maxRotatespeed;

        // 자폭 시작
        StartCoroutine(Explosive());
    }

    protected override void OnMoveUpdate(float deltaTime)
    {
        transform.Translate(deltaTime * speed * direction, Space.World);
        transform.Rotate(0, 0, deltaTime * rotateSpeed);
    }

    /// <summary>
    /// 목적지 설정하는 함수
    /// </summary>
    /// <param name="destination">목적지(월드 좌표)</param>
    public void SetDestination(Vector3 destination)
    {
        direction = (destination - transform.position).normalized;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + direction);
    }

    /// <summary>
    /// 큰 운석을 일정 시간이 지나면 disable 하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator Explosive()
    {
        yield return new WaitForSeconds(explosiveTime);
        gameObject.SetActive(false);
    }
}

// 기본 기능 필요
// 자폭
// 죽을 때 작은 운석 생성(랜덤한 개수)

