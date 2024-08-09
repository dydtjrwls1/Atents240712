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
    public float maxExplosiveTime = 7.0f;

    // 회전 속도 랜덤 분포용 커브
    public AnimationCurve rotateSpeedCurve;

    // 자폭 표시 색 결정용 커브
    public AnimationCurve explosiveCurve;

    // 생성할 작은 운석의 개수 범위
    public int minSmallCount = 3;
    public int maxSmallCount = 8;

    // 크리티컬 확률
    [Range(0f, 1f)]
    public float criticalRate = 0.05f;

    // 크리티컬 배율
    [Min(1.0f)]
    public float criticalMultiplier = 3.0f;

    // 최종 회전 속도
    float rotateSpeed;

    // 자폭 까지의 시간
    float explosiveTime;

    // 자폭 진행 시간
    float explosiveElapsed = 0.0f;

    // 이동 방향
    Vector3 direction;

    // 원래 점수 저장용 변수
    int orgPoint = 0;

    // 운석 스프라이트 렌더러
    SpriteRenderer sr;
    
    private void Awake()
    {
        orgPoint = point; // 자폭 대비 원점수 미리 저장
        sr = GetComponent<SpriteRenderer>();
    }

    protected override void OnReset()
    {
        base.OnReset();
        point = orgPoint; // 원래 점수로 복원
        speed = Random.Range(minMoveSpeed, maxMoveSpeed); // 이동 속도 랜덤
        rotateSpeed = minRotateSpeed + rotateSpeedCurve.Evaluate(Random.value) * maxRotatespeed; // 회전속도 랜덤

        explosiveElapsed = 0.0f; // 누적 시간 초기화
        sr.color = Color.white; // 랜더러 색 초기화

        // 자폭 시작
        StartCoroutine(SelfExplosive());
    }

    protected override void OnMoveUpdate(float deltaTime)
    {
        transform.Translate(deltaTime * speed * direction, Space.World);
        transform.Rotate(0, 0, deltaTime * rotateSpeed);
    }

    protected override void OnVisualUpdate(float deltaTime)
    {
        explosiveElapsed += deltaTime;
        // 진행율 : explosiveTime / explosiveElapsed;
        // 시작색 : Color(1, 1, 1) 
        // 마지막색 : Color(1, 0, 0)
        explosiveCurve.Evaluate(explosiveElapsed);
        sr.color = Color.Lerp(Color.white, Color.red, explosiveCurve.Evaluate(explosiveElapsed / explosiveTime));
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
    IEnumerator SelfExplosive()
    {
        explosiveTime = Random.Range(minExplosiveTime, maxExplosiveTime);
        yield return new WaitForSeconds(explosiveTime);

        point = 0;

        Die();
    }

    /// <summary>
    /// 큰 운석이 터질 때 일어날 일을 기록해놓은 함수
    /// </summary>
    protected override void OnDie()
    {
        int count = Random.Range(minSmallCount, maxSmallCount);
        if (Random.value < criticalRate)
            count = Mathf.RoundToInt(maxSmallCount * criticalMultiplier);

        float angleDiff = 360 / count;
        Vector3 dir = Quaternion.Euler(0, 0, Random.Range(0, 360)) * Vector3.left; // 왼쪽 벡터를 랜덤하게 돌린 것이 기준방향
        for (int i = 0; i < count; i++)
        {
            Quaternion q = Quaternion.Euler(0, 0, angleDiff * i);
            Factory.Instance.GetEnemyAsteroidSmall(transform.position, q * dir);
        }
    }
}

// 기본 기능 필요
// 자폭
// 죽을 때 작은 운석 생성(랜덤한 개수)

