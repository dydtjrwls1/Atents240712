using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyAsteroidSmall : EnemyBase
{
    [Header("작은 운석 데이터")]
    float baseSpeed;
    
    // 속도 범위 ( +-speedRandomRange ) 
    float speedRandomRange = 1.0f;

    // 작은 운석의 회전속도
    float rotateSpeed;

    // 이동 방향 ( 처음에만 설정되고 값이 바뀌지 않기를 원하기 때문에 nullable로 선언한다 )
    Vector3? direction = null;

    public Vector3 Direction
    {
        private get => direction.GetValueOrDefault(); // 읽기는 private
        set                                          // 쓰기는 public 이지만 한번만 설정 가능
        {
            if (direction == null)
                direction = value.normalized;
        }
    }

    private void Awake()
    {
        baseSpeed = speed;
    }

    protected override void OnReset()
    {
        base.OnReset();

        speed = baseSpeed + Random.Range(-speedRandomRange, speedRandomRange);
        rotateSpeed = Random.Range(0, 360);
        direction = null;            // Reset 이후에 Direction 에 한번 값을 넣을 수 있도록 설정
    }

    protected override void OnMoveUpdate(float deltaTime)
    {
        transform.Translate(deltaTime * speed * Direction, Space.World);    // 초당 speed 의 속도로 Direciton 방향으로 이동
        transform.Rotate(deltaTime * rotateSpeed * Vector3.forward);
    }
}
