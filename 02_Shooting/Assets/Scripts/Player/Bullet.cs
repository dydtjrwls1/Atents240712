using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : RecycleObject
{
    // 총알의 이동속도
    public float speed = 10.0f;

    // 총알의 수명
    public float lifeTime = 10.0f;

    // 총알이 맞았을 때 이펙트
    public GameObject hitEffect;

    private void Start()
    {
        // Destroy(gameObject, lifeTime);
        DisableTimer(lifeTime);
    }
    // Update is called once per frame
    void Update()
    {
        // 내 위치에서 초당 speed 의 속도로 local 오른쪽 방향으로 이동한다.
        // transform.position += Time.deltaTime * speed * transform.right;

        // 초당 speed 의 속도로, 월드 기준으로 내 오른쪽 방향으로 이동하기.
        // transform.Translate(Time.deltaTime * speed * transform.right, Space.World);

        // 로컬 기준으로 오른쪽 방향으로 이동하기
        // transform.Translate(Time.deltaTime * speed * Vector3.right, Space.Self); // default 값은 Space.Self 이다.
        // transform.Translate(Time.deltaTime * speed, 0, 0);

        transform.Translate(Time.deltaTime * speed * Vector3.right);

    }

    // 충돌이 시작 되었을 때 실행
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("충돌 시작");
        // Instantiate(hitEffect, transform.position, Quaternion.identity);
        SimpleFactory.Instance.GetHit(transform.position);

        // Destroy(gameObject); // 자기자신 제거하기
        gameObject.SetActive(false);
    }

    //// 충돌이 된 상태에서 움직임이 있을 때 실행
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    // Debug.Log("충돌 중");
    //}

    //// 충돌이 끝났을 때 실행
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    // Debug.Log("충돌 종료");
    //}

    // 겹침이 시작 되었을 때
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
        
    //}

    //private void OnTriggerStay2D(Collider2D collision)
    //{
        
    //}

    //// 겹침이 끝났을 때
    //private void OnTriggerExit2D(Collider2D collision)
    //{
        
    //}
}
