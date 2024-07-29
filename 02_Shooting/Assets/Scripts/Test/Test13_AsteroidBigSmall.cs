using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test13_AsteroidBigSmall : TestBase
{
    public Transform spawnPosition;
    public Transform moveTarget;

    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        Factory.Instance.GetEnemyAsteroidBig(spawnPosition.position, spawnPosition.position + Vector3.left);
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        Factory.Instance.GetEnemyAsteroidSmall(spawnPosition.position, moveTarget.position - spawnPosition.position);
    }
}
