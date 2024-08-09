using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test21_Boss : TestBase
{
    public Transform target;

    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        Factory.Instance.GetBossBullet(target.position);
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        Factory.Instance.GetBoss(target.position);
    }
}
