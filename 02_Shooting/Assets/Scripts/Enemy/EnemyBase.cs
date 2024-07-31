using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemyBase : RecycleObject
{
    [Header("적 기본 데이터")]
    // 적의 수명
    public float lifeTime = 30.0f;

    // 적의 이동속도
    public float speed = 5.0f;

    // 이 적을 죽였을 때 얻는 점수
    public int point = 10;

    // 최대 HP
    public int maxHP = 1;

    // 적의 HP
    int hp = 1;

    // 살아 있는 여부
    bool isAlive = true;

    // 자신이 죽었음을 알리는 델리게이트(int : 자신의 점수)
    public Action<int> onDie;

    public int HP
    {
        // get { return hp; }
        get => hp;      // 읽기는 public
        private set     // 쓰기는 private
        {
            hp = value;
            if (hp < 1) // 0이 되면
            {
                Die(); // 사망 처리 수행
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        OnMoveUpdate(Time.deltaTime);
        OnVisualUpdate(Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HP--; // HP = HP - 1 // HP를 get 한 다음 -1 을 처리하고 다시 set하기
    }
    protected override void OnReset()
    {
        HP = maxHP;
        isAlive = true;
        DisableTimer(lifeTime);
    }

    /// <summary>
    /// Enemy 종류별로 비주얼 변경 처리를 하는 함수
    /// </summary>
    /// <param name="deltaTime"></param>
    protected virtual void OnVisualUpdate(float deltaTime) { }


    /// <summary>
    /// Enemy의 종류별로 이동처리를 하는 함수
    /// </summary>
    /// <param name="deltaTime">TIme.deltaTime</param>
    protected virtual void OnMoveUpdate(float deltaTime)
    {
        transform.Translate(deltaTime * speed * -transform.right, Space.World); // 기본 동작은 왼쪽으로 계속 이동하기
    }

    protected void Die()
    {
        if (isAlive) // 살아 있을 때만 죽일 수 있음
        {
            isAlive = false; // 죽었다고 표시

            onDie?.Invoke(point); // 죽었다고 등록된 객체들에게 알리기(등록된 함수 실행)

            Factory.Instance.GetExplosion(transform.position);

            OnDie();        

            DisableTimer(); // 자기 자신을 비활성화 시키기
        }
    }

    /// <summary>
    /// 죽었을 때 적의 종류별로 실행해야 할 일을 수행하는 함수(빈 함수)
    /// </summary>
    protected virtual void OnDie()
    {

    }
}
