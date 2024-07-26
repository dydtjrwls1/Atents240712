using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test13_AsteroidBigSmall : TestBase
{
    public Transform target;
    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        Factory.Instance.GetEnemyAsteroidBig(target.position, target.position + Vector3.left);
    }
}
