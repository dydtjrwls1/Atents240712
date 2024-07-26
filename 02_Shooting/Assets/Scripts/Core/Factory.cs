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
    }

    // 풀에서 오브젝트 가져오는 함수들 ======================================================================
    public Bullet GetBullet(Vector3? position, float angle = 0.0f)
    {
        return bullet.GetObject(position, new Vector3(0, 0, angle)); // = Vector3.forward * angle
    }

    public EnemyOld GetEnemy(Vector3? position, float angle = 0.0f)
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

    public AsteroidOld GetAsteroid(Vector3? position)
    {
        return asteroid.GetObject(position);
    }

    public EnemyWave GetEnemyWave(Vector3? position)
    {
        return enemyWave.GetObject(position);
    }
}
