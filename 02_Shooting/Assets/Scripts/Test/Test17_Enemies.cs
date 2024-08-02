using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test17_Enemies : TestBase
{
    public Transform target;

    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        Factory.Instance.GetEnemyCurve(target.position);
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        Factory.Instance.GetEnemyBonus(target.position);
    }
}
