using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCurve : EnemyBase
{
    [Header("커브 적 정보")]
    public float rotateSpeed = 5.0f;

    private void Start()
    {
        rotateSpeed = transform.position.y > 0.0f ? rotateSpeed : -rotateSpeed;
    }

    protected override void OnMoveUpdate(float deltaTime)
    {
        transform.Rotate(0, 0, deltaTime * rotateSpeed);
       
        transform.Translate(deltaTime * speed * -transform.right);
    }
}
