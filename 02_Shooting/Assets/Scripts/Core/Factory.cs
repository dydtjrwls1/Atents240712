using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : SingleTon<Factory>
{
    BulletPool bullet;
    OldEnemyPool enemy;
    HitEffectPool hit;
    ExplosionEffectPool explosion;
    OldAsteroidPool asteroid;
    EnemyWavePool enemyWave;
    EnemyAsteroidBIgPool enemyAsteroidBig;
    EnemyAsteroidSmallPool enemyAsteroidSmall;  

    protected override void OnInitialize()
    {
        // 풀 초기화
        bullet = GetComponentInChildren<BulletPool>();
        if(bullet != null) 
            bullet.Initialize();

        enemy = GetComponentInChildren<OldEnemyPool>();
        if (enemy != null)
            enemy.Initialize();

        hit = GetComponentInChildren<HitEffectPool>();
        if (hit != null)
            hit.Initialize();

        explosion = GetComponentInChildren<ExplosionEffectPool>();
        if (explosion != null) 
            explosion.Initialize();

        asteroid = GetComponentInChildren<OldAsteroidPool>();
        if (asteroid != null)
            asteroid.Initialize();

        enemyWave = GetComponentInChildren<EnemyWavePool>();
        if (enemyWave != null)
            enemyWave.Initialize();

        enemyAsteroidBig = GetComponentInChildren<EnemyAsteroidBIgPool>();
        if (enemyAsteroidBig != null)
            enemyAsteroidBig.Initialize();

        enemyAsteroidSmall = GetComponentInChildren<EnemyAsteroidSmallPool>();
        if (enemyAsteroidSmall != null)
            enemyAsteroidSmall.Initialize();
    }

    // 풀에서 오브젝트 가져오는 함수들 ======================================================================
    public Bullet GetBullet(Vector3? position, float angle = 0.0f)
    {
        return bullet.GetObject(position, new Vector3(0, 0, angle)); // = Vector3.forward * angle
    }

    public OldEnemy GetEnemy(Vector3? position, float angle = 0.0f)
    {
        return enemy.GetObject(position, new Vector3(0, 0, angle));
    }

    public explosion GetHit(Vector3? position)
    {
        return hit.GetObject(position);
    }

    public explosion GetExplosion(Vector3? position)
    {
        return explosion.GetObject(position);
    }

    public OldAsteroid GetAsteroid(Vector3? position)
    {
        return asteroid.GetObject(position);
    }

    public EnemyWave GetEnemyWave(Vector3? position)
    {
        return enemyWave.GetObject(position);
    }

    /// <summary>
    /// 큰 운석 하나를 돌려주는 함수
    /// </summary>
    /// <param name="position">생성위치</param>
    /// /// <param name="direction">이동 방향</param>
    /// <param name="angle">초기각도</param>
    /// <returns>큰 운석 하나</returns>
    public EnemyAsteroidBig GetEnemyAsteroidBig(Vector3? position, Vector3? targetPosition = null, float? angle = 0.0f)
    {
        // dir 이 null 이면 vector3.left 값을 사용, null 이 아니면 direction 이 들어있는 값을 사용.
        Vector3 dir = targetPosition ?? position.GetValueOrDefault() + Vector3.left;
        Vector3 euler = Vector3.zero;
        euler.z = angle ?? Random.Range(0.0f, 360.0f); // 초기 회전 정도 지정

        EnemyAsteroidBig big = enemyAsteroidBig.GetObject(position, euler);
        big.SetDestination(dir);

        return big;
    }
    public EnemyAsteroidSmall GetEnemyAsteroidSmall(Vector3? position, Vector3? targetPosition = null, float? angle = 0.0f)
    {
        // dir 이 null 이면 vector3.left 값을 사용, null 이 아니면 direction 이 들어있는 값을 사용.
        Vector3 dir = targetPosition ?? position.GetValueOrDefault() + Vector3.left;
        Vector3 euler = Vector3.zero;
        euler.z = angle ?? Random.Range(0.0f, 360.0f); // 초기 회전 정도 지정

        EnemyAsteroidSmall small = enemyAsteroidSmall.GetObject(position, euler);
        small.SetDestination(dir);

        return small;
    }
}