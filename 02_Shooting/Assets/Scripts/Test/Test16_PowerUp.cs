using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test16_PowerUp : TestBase
{
    public Transform target;

    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        Factory.Instance.GetPowerUp(target.position);
    }
}
