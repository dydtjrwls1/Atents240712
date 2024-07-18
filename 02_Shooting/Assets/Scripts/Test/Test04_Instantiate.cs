using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test04_Instantiate : TestBase
{
    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        int i = Random.Range(0, 10);            // 0~9 중 하나의 int를 리턴한다.
        float f = Random.Range(0.0f, 10.0f);    // 0.0~10.0 중 하나의 float 을 리턴한다.
        float f2 = Random.value;                // 0.0~1.0 중 하나의 float 을 리턴한다.
    }
}
