using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBoss : EnemyBase
{
    // 총알은 주기적으로 발사(Fire1, Fire2 위치)
    // 미사일은 방향전환을 할 때마다 일정 수만큼 연사
    [Header("보스 데이터")]
    public float barrageInterval = 0.2f;

    public float bulletInterval = 1.0f;

    public int barrageCount = 3;

    public Vector2 areaMin = new Vector2(2, -3);
    public Vector2 areaMax = new Vector2(7, 3);

    float currentY;

    bool onPattern = false;

    Transform fire1;
    Transform fire2;
    Transform fire3;

    private void Awake()
    {
        Transform firePosition = transform.GetChild(1);
        fire1 = firePosition.GetChild(0);       
        fire2 = firePosition.GetChild(1);       
        fire3 = firePosition.GetChild(2);       
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(StartRoutine());   
    }

    protected override void OnMoveUpdate(float deltaTime)
    {
        if (!onPattern)
        {
            base.OnMoveUpdate(deltaTime);
        }
    }

    IEnumerator StartRoutine()
    {
        yield return new WaitForSeconds(1.7f);
        speed = 0.0f;
        StartCoroutine(MovePattern());
    }

    IEnumerator MovePattern()
    {
        speed = 3.0f;
        currentY = areaMin.y;
        float randX = Random.Range(areaMin.x, areaMax.x);
        Vector3 randPos = new Vector3(randX, currentY, 0);
        onPattern = true;
        StartCoroutine(BulletFire(bulletInterval));
 
        while (true)
        {
            if(Mathf.Abs(transform.position.y) > Mathf.Abs(currentY))
            {
                randX = Random.Range(areaMin.x, areaMax.x);
                currentY = -currentY;
                randPos = new Vector3(randX, currentY, 0);

                StartCoroutine(MissileFire(barrageCount, barrageInterval));
            }

            transform.Translate(Time.deltaTime * speed * (randPos - transform.position).normalized);

            yield return null;
        }
        

    }

    IEnumerator BulletFire(float interval)
    {
        while (true)
        {
            Factory.Instance.GetBossBullet(fire1.position);
            Factory.Instance.GetBossBullet(fire2.position);

            yield return new WaitForSeconds(interval);
        }
    }

    IEnumerator MissileFire(int count, float interval)
    {
        for(int i = 0; i < count; i++)
        {
            Factory.Instance.GetBossMissle(fire3.position);
            yield return new WaitForSeconds(interval);
        }
    }
}
