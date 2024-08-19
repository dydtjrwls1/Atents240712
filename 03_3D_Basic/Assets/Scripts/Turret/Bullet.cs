using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : RecycleObject
{
    public float initialSpeed = 20.0f;
    public float lifeTime = 10.0f;

    // 발사 후 충돌이 있었는지 확인하는 변수
    bool isConflict = false;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        isConflict = true;
        StopAllCoroutines(); // 부딪히면 이전 코루틴 정지
        DisableTimer(2.0f);  // 새로 2초뒤에 사라지게 설정
    }

    private void FixedUpdate()
    {
        // 벡터의 길이는 Vector3.magnitude로 얻을 수 있다.
        // 다만 연산에 많은 시간이 걸리므로 가능하면 Vector3.sqrMagnitude(root 연산을 하지 않은 크기)를 사용해야 한다.
        if(!isConflict) // 어느정도 이동하고 있으면
        {
            transform.forward = rb.velocity; // 움직이는 방향으로 forward 설정
        }
    }

    protected override void OnReset()
    {
        DisableTimer(lifeTime);
        rb.angularVelocity = Vector3.zero;
        rb.velocity = initialSpeed * transform.forward;
        isConflict = false;
    }

    // 총알이 날아갈 때 앞으로 기울어지게 만들기
}
