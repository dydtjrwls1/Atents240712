using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test08_ObjectPool : TestBase
{
    public BulletPool bulletPool;
    public EffectPool explosionEffectPool;
    public EffectPool hitEffectPool;
    public EnemyPool enemyPool;

    // Start is called before the first frame update
    void Start()
    {
        bulletPool.Initialize();
        explosionEffectPool.Initialize();
        hitEffectPool.Initialize();
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
            SimpleFactory.Instance.GetEnemy();
            yield return null;
        }
    }
}
