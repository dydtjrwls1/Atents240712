using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test01_Sprite2 : TestBase
{
    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        //base; 부모에 접근하기 위한 키워드
        //this; 자기 자신에 접근하기 위한 키워드

        //base.Test1_performed(context); // 부모의 기능도 같이 실행하는 경우

        Debug.Log("자식 Test1");
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        int rand = Random.Range(0, 10);
        Debug.Log($"랜덤 : {rand}");  
    }

    protected override void TestWASD_performed(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        Debug.Log($"입력 {input}");
    }
}
