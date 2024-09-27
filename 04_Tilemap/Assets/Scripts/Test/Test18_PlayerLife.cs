using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test18_PlayerLife : TestBase
{
    public ImageNumber imageNumber;

    public int number;

    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        imageNumber.Number = number;
    }
}
