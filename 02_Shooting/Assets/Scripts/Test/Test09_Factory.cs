using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test09_Factory : TestBase
{
    public enum ObjectType
    {
        Bullet,
        HitEffect,
        ExplosionEffect,
        Enemy
    }

    public ObjectType spawnType = ObjectType.Bullet;

    public Transform target;

    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        switch (spawnType)
        {
            case ObjectType.Bullet:
                Factory.Instance.GetBullet(target.position);
                break;
            case ObjectType.HitEffect:
                Factory.Instance.GetHit(target.position);
                break;
            case ObjectType.ExplosionEffect:
                Factory.Instance.GetExplosion(target.position);
                break;
            case ObjectType.Enemy:
                Factory.Instance.GetEnemy(target.position);
                break;
        }
    }
}
