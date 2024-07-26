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
        Asteroid asteroid = Factory.Instance.GetAsteroid(new Vector3(5, 5));
        asteroid.SetDestination(target.position);
    }
}
