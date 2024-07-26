using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test08_ObjectPool : TestBase
{
    public enum ObjectType
    {
        Bullet,
        HitEffect,
        ExplosionEffect,
        Enemy
    }

    public BulletPool bulletPool;
    public HitEffectPool hitEffectPool;
    public ExplosionEffectPool expsionEffectPool;
    public OldEnemyPool enemyPool;

    public ObjectType spawnType;

    // Start is called before the first frame update
    void Start()
    {
        bulletPool.Initialize();
        hitEffectPool.Initialize();
        expsionEffectPool.Initialize();
        enemyPool.Initialize();
    }



    protected override void Test1_performed(InputAction.CallbackContext _)
    {
        StopAllCoroutines();
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        StartCoroutine(PoolSpawn());
    }

    protected override void Test3_performed(InputAction.CallbackContext context)
    {
        StartCoroutine(InstantiateSpawn());
    }

    protected override void Test4_performed(InputAction.CallbackContext context)
    {
        switch (spawnType)
        {
            case ObjectType.Bullet:
                bulletPool.GetObject();
                break;
            case ObjectType.HitEffect:
                hitEffectPool.GetObject();
                break;
            case ObjectType.ExplosionEffect:
                expsionEffectPool.GetObject();
                break;
            case ObjectType.Enemy:
                enemyPool.GetObject();
                break;
        }
    }

    IEnumerator PoolSpawn()
    {
        while (true)
        {
            bulletPool.GetObject();
            yield return null;
        }
    }

    IEnumerator InstantiateSpawn()
    {
        while(true)
        {
            SimpleFactory.Instance.Getbullet();
            yield return null;
        }
    }
}
