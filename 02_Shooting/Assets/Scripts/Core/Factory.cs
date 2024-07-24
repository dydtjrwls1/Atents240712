using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : SingleTon<Factory>
{
    BulletPool bullet;
    EnemyPool enemy;
    HitEffectPool hit;
    ExplosionEffectPool explosion;

    protected override void OnInitialize()
    {
        // 풀 초기화
        bullet = GetComponentInChildren<BulletPool>();
        if(bullet != null) 
            bullet.Initialize();

        enemy = GetComponentInChildren<EnemyPool>();
        if (enemy != null)
            enemy.Initialize();

        hit = GetComponentInChildren<HitEffectPool>();
        if (hit != null)
            hit.Initialize();

        explosion = GetComponentInChildren<ExplosionEffectPool>();
        if (explosion != null) 
            explosion.Initialize();
    }

    // 풀에서 오브젝트 가져오는 함수들 ======================================================================
    public Bullet GetBullet(Vector3? position, float angle = 0.0f)
    {
        return bullet.GetObject(position, new Vector3(0, 0, angle)); // = Vector3.forward * angle
    }

    public Enemy GetEnemy(Vector3? position, float angle = 0.0f)
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
}
