using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    // 총알의 이동속도
    public float speed = 10.0f;

    // 총알의 수명
    public float lifeTime = 10.0f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
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
}
