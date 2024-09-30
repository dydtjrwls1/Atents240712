using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test19_GameOver : TestBase
{
    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        Time.timeScale = 0.05f;
    }
}
