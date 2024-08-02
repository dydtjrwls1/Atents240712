using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCurve : EnemyBase
{
    [Header("커브 도는 적 데이터")]
    public float rotateSpeed = 10.0f;

    // 회전 방향 ( 1이면 반시계 방향 -1이면 시계 방향 )
    float curveDirection = 1.0f;

    protected override void OnMoveUpdate(float deltaTime)
    {
        base.OnMoveUpdate(deltaTime);
        transform.Rotate(deltaTime * rotateSpeed * curveDirection * Vector3.forward);
    }

    public void UpdateRotateDirection()
    {
        if (transform.position.y < 0.0f)
            curveDirection = -1.0f;
        else
            curveDirection = 1.0f;
    }
}
