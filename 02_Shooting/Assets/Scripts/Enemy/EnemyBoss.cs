using System.Collections;
using UnityEngine;

public class EnemyBoss : EnemyBase
{
    // 총알은 주기적으로 발사(Fire1, Fire2 위치)
    // 미사일은 방향전환을 할 때마다 일정 수만큼 연사
    [Header("보스 데이터")]
    public float barrageInterval = 0.2f;

    public float bulletInterval = 1.0f;

    public int barrageCount = 3;

    public Vector2 areaMin = new Vector2(2, -3);
    public Vector2 areaMax = new Vector2(7, 3);

    Transform fire1;
    Transform fire2;
    Transform fire3;

    Vector3 moveDirection = Vector3.left;

    private void Awake()
    {
        Transform fire = transform.GetChild(1);
        fire1 = fire.GetChild(0);
        fire2 = fire.GetChild(1);
        fire3 = fire.GetChild(2);
    }

    protected override void OnReset()
    {
        base.OnReset();

        StartCoroutine(MovePatternProcess());
    }


    protected override void OnMoveUpdate(float deltaTime)
    {
        transform.Translate(deltaTime * speed * moveDirection);
    }



    IEnumerator MovePatternProcess()
    {
        moveDirection = Vector3.left;

        yield return null;      // 꺼냈을 때 OnReset이 먼저 실행된 후 위치설정을 하기 때문에, 위치 설정 이후에 아래코드가 실행되도록 한 프레임 대기.

        float middleX = (areaMax.x - areaMin.x) * 0.5f + areaMin.x; // area 의 가운데 위치

        while(transform.position.x > middleX)
        {
            yield return null; // 보스의 x 위치가 middle 보다 왼쪽에 갈 때까지 대기
        }

        StartCoroutine(FireBulletCoroutine());
        ChangeDirection(); // 일단 방향전환

        while (true)
        {
            if (transform.position.y > areaMax.y || transform.position.y < areaMin.y)       // 범위를 벗어나면 방향 전환한다.
            {
                ChangeDirection();
                StartCoroutine(FireMissileCoroutine());
            }
                
            yield return null;
        }
    }

    IEnumerator FireBulletCoroutine()
    {
        while(true)
        {
            Factory.Instance.GetBossBullet(fire1.position);
            Factory.Instance.GetBossBullet(fire2.position);
            yield return new WaitForSeconds(bulletInterval);
        }
    }

    /// <summary>
    /// 보스의 이동 방향을 변경하는 함수
    /// </summary>
    void ChangeDirection()
    {
        Vector3 target = new Vector3();
        target.x = Random.Range(areaMin.x, areaMax.x);  // x 위치는 areaMin.x ~ areaMax.x 
        target.y = (transform.position.y > areaMax.y) ? areaMin.y : areaMax.y; // areaMax 보다 위로 갔으면 아래로, areaMin 보다 아래로 갔으면 위로

        moveDirection = (target - transform.position).normalized; // 방향 변경
    }

    IEnumerator FireMissileCoroutine()
    {
        for (int i = 0; i < barrageCount; i++)
        {
            Factory.Instance.GetBossMissle(fire3.position);
            yield return new WaitForSeconds(barrageInterval);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Vector3 p0 = new (areaMin.x, areaMin.y);
        Vector3 p1 = new (areaMax.x, areaMin.y);
        Vector3 p2 = new (areaMax.x, areaMax.y);
        Vector3 p3 = new (areaMin.x, areaMax.y);

        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p0);
    }
#endif
}
