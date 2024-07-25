using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : RecycleObject
{
    // 적의 수명
    public float lifeTime = 30.0f;

    // 적의 이동속도
    public float speed = 5.0f;

    // awake 시 y 위치
    float init_Y;

    // 위 아래 방향 결정
    // float direction_Y = 1.0f;

    // 위 아래 y축 경계선
    // float limit_Y = 3.0f;

    float elapsedTime = 0.0f;

    public float frequency = 2.0f;

    public float amplitude = 3.0f;

    // 적 기 격추 시 터지는 모션
    public GameObject explosion;

    // 적의 HP
    int hp = 2;

    /// <summary>
    /// 적의 HP 를 get/set 할 수 있는 프로퍼티
    /// </summary>
    public int HP
    {
        // get { return hp; }
        get => hp;      // 읽기는 public
        private set     // 쓰기는 private
        {
            hp = value;
            if (hp < 1) // 0이 되면
            {
                OnDie(); // 사망 처리 수행
            }
        }
    }

    // 살아 있는 여부
    bool isAlive = true;

    // 이 적을 죽였을 때 얻는 점수
    public int point = 10;

    // float spawnY = 0.0f;
    private void Start()
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //hp--;
        //if (hp <= 0) 
        //{
        //    OnDie();
        //} 
        HP--; // HP = HP - 1 // HP를 get 한 다음 -1 을 처리하고 다시 set하기
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        DisableTimer(lifeTime);
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

    /// <summary>
    /// 적 개체가 터질 때 실행될 함수.
    /// </summary>
    void OnDie()
    {
        if (isAlive) // 살아 있을 때만 죽일 수 있음
        {
            isAlive = false; // 죽었다고 표시

            ScoreText scoreText = FindAnyObjectByType<ScoreText>();
            scoreText.AddScore(point);
            Factory.Instance.GetExplosion(transform.position);

            // Destroy(gameObject); // 자기 자신 삭제
            DisableTimer();
            // Instantiate(explosion, transform.position, Quaternion.identity); // 터지는 이펙트 나오기
        }
    }
}
