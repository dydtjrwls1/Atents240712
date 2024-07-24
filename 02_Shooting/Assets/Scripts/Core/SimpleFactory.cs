using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Factory 타입의 싱글톤
public class SimpleFactory : SingleTon<SimpleFactory>
{
    public EnemyPool enemyPool;
    public GameObject bulletPrefab;
    public GameObject hitPrefab;
    public GameObject explosionEffectPrefab;

    //public GameObject GetEnemy()
    //{
    //    return Instantiate(enemyPrefab);
    //}

    public GameObject GetEnemy(Vector3? position = null, float angle = 0.0f)
    {
        Enemy enemy = enemyPool.GetObject();
        return enemy.gameObject;
        // return Instantiate(enemyPrefab, position.GetValueOrDefault() , Quaternion.Euler(0,0,angle));
    }
    public GameObject Getbullet(Vector3? position = null, float angle = 0.0f)
    {
        return Instantiate(bulletPrefab, position.GetValueOrDefault(), Quaternion.Euler(0, 0, angle));
    }
    public GameObject GetHit(Vector3? position = null, float angle = 0.0f)
    {
        return Instantiate(hitPrefab, position.GetValueOrDefault(), Quaternion.Euler(0, 0, angle));
    }
    public GameObject GetExplosion(Vector3? position = null, float angle = 0.0f)
    {
        return Instantiate(explosionEffectPrefab, position.GetValueOrDefault(), Quaternion.Euler(0, 0, angle));
    }
}
