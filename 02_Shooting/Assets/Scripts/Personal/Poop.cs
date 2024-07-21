using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MyEnemy
{
    // 가속력
    float lerpSpeed = 0.0f;

    // 가속력
    float accelSpeed;

    // 가속력 범위
    const float minAccel = 1.0f;
    const float maxAccel = 10.0f;

    // 회전 속도
    public float rotationSpeed = 30.0f;

    // lerp 보간 간격
    const float _inter = 0.002f;

    // 회전 방향
    float rotationDirect = 1.0f;

    private void Start()
    {
        fallSpeed = 3.0f;
        int randomInt = UnityEngine.Random.Range(0, 2);
        if (randomInt == 0) { rotationDirect = -1.0f; }
        accelSpeed = UnityEngine.Random.Range(minAccel, maxAccel);
        Destroy(gameObject, destroyInterval);
    }
    // Update is called once per frame
    void Update()
    {
        lerpSpeed = Mathf.Lerp(lerpSpeed, accelSpeed, _inter);
        transform.Translate(Time.deltaTime * fallSpeed * lerpSpeed * Vector2.down, Space.World);
        transform.Rotate(Time.deltaTime * rotationDirect * rotationSpeed * accelSpeed * Vector3.forward, Space.World);
    }
}
