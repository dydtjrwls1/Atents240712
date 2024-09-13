using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test06_SlimePool : TestBase
{
    float rangeX = 8.0f;
    float rangeY = 4.0f;

    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        // 팩토리에서 슬라임 하나 꺼내기 (위치는 보이는 영역 안에 랜덤하게) (-8, -4) ~ (8, 4) 사이 
        float randX = Random.Range(-rangeX, rangeX);
        float randY = Random.Range(-rangeY, rangeY);
        Vector3 position = new Vector3(randX, randY, 0.0f);
        Factory.Instance.GetSlime(position);
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        // Scene 내의 모든 슬라임의 Outline 보이기
    }

    protected override void Test3_performed(InputAction.CallbackContext context)
    {
        // Scene 내의 모든 슬라임의 Outline 감추기
    }

    protected override void Test4_performed(InputAction.CallbackContext context)
    {
        // Scene 내의 모든 슬라임 사망
    }
}
