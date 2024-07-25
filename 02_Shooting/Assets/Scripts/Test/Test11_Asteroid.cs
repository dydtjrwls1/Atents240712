using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test11_Asteroid : TestBase
{

    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        Factory.Instance.GetAsteroid(transform.position);

    }
}
