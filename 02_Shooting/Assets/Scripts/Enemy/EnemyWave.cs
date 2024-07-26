using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : EnemyBase
{
    // awake 시 y 위치
    float init_Y;

    float elapsedTime = 0.0f;

    public float frequency = 2.0f;

    public float amplitude = 3.0f;

    private void Start()
    {
        init_Y = transform.position.y;
    }

    // Wave 용 이동처리
    protected override void OnMoveUpdate(float deltaTime)
    {
        elapsedTime += deltaTime * frequency;

        // transform.Translate(deltaTime * speed * Vector3.left);

        transform.position = new Vector3(transform.position.x - deltaTime * speed, init_Y + MathF.Sin(elapsedTime) * amplitude, 0.0f);
    }
}
