using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : RecycleObject
{
    public float initialSpeed = 20.0f;
    public float lifeTime = 10.0f;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        StopAllCoroutines(); // 부딪히면 이전 코루틴 정지
        DisableTimer(2.0f);  // 새로 2초뒤에 사라지게 설정
    }

    protected override void OnReset()
    {
        DisableTimer(lifeTime);
        rb.velocity = initialSpeed * transform.forward;
    }

    // 총알이 날아갈 때 앞으로 기울어지게 만들기
}
