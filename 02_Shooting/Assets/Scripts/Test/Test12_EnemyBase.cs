using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test12_EnemyBase : TestBase
{
    Transform target;
    private void Start()
    {
        target = transform.GetChild(0);
    }

    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        Factory.Instance.GetEnemyWave(target.position);
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        Factory.Instance.GetEnemyAsteroidBig(target.position, Vector3.up);
    }
}
