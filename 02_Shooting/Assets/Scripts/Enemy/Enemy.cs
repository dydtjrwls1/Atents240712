using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 적의 이동속도
    protected float speed = 5.0f;

    // awake 시 y 위치
    float init_Y;

    // 위 아래 방향 결정
    // float direction_Y = 1.0f;

    // 위 아래 y축 경계선
    // float limit_Y = 3.0f;

    float elapsedTime = 0.0f;

    public float frequency = 2.0f;

    public float amplitude = 3.0f;

    // float spawnY = 0.0f;
    private void Awake()
    {
        init_Y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // pos_Y = Mathf.Cos(transform.position.x);
        // transform.Translate(Time.deltaTime * speed * new Vector3(-1, pos_Y, 0));

        //if (transform.position.y - init_Y >= limit_Y)
        //{
        //    direction_Y *= -1.0f; // y 방향 반전
        //} else if (transform.position.y - init_Y <= -limit_Y)
        //{
        //    direction_Y *= -1.0f; // y 방향 반전
        //}

        //Move(direction_Y);


        MoveUpdate(Time.deltaTime);
    }


    //void Move(float direction)
    //{
    //    transform.Translate(Time.deltaTime * speed * ((Vector3.up * direction) + Vector3.left));
    //}

    void MoveUpdate(float deltaTime)
    {
        elapsedTime += deltaTime * frequency;

        // transform.Translate(deltaTime * speed * Vector3.left);

        transform.position = new Vector3(transform.position.x - deltaTime * speed, init_Y + MathF.Sin(elapsedTime) * amplitude, 0.0f);
    }
}
