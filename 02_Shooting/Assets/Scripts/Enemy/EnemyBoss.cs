using System.Collections;
using System.Collections.Generic;
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

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnMoveUpdate(float deltaTime)
    {
        base.OnMoveUpdate(deltaTime);
    }

    IEnumerator StartRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        speed = 0.0f;
    }
}
