using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    // 낙하 속도
    public float fallSpeed = 2.0f;

    // 가속도 계수
    float lerpSpeed = 0.0f;

    // 가속력
    float accelSpeed = 3.0f;

    // 회전 속도
    public float rotationSpeed = 30.0f;

    private void Start()
    {
        Destroy(gameObject, 5.0f);
    }
    // Update is called once per frame
    void Update()
    {
        lerpSpeed = Mathf.Lerp(lerpSpeed, accelSpeed, Time.deltaTime);
        transform.Translate(Time.deltaTime * fallSpeed * lerpSpeed * Vector2.down, Space.World);
        transform.Rotate(Time.deltaTime * rotationSpeed * Vector3.forward, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
