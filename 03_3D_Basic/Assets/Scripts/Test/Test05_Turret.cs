using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test05_Turret : TestBase
{
    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        Quaternion a = Quaternion.identity; // 아무것도 하지 않는 회전
        a = Quaternion.Euler(90, 0, 0);     // 오일러 각을 이용한 회전 만들기
        a = Quaternion.LookRotation(transform.forward); // 특정 방향을 바라보게 만드는 회전 만들기
        a = Quaternion.FromToRotation(Vector3.forward, Vector3.right); // from 에서 to 로 가는 회전 만들기
        a = Quaternion.Inverse(a);          // 역회전 만들기
        // Quaternion.Angle(a, b);          // 두 회전 사이의 각도
        // Quaternion.RotateTowards(a, b, 30.0f); // from 에서 to 로 회전하는데 최대 delta 만큼만 회전 (회전에 제한을 건다)
        // Quaternion.Slerp(a, b, t); // a 에서 b 로 t 만큼의 비율로 회전 (회전 보간)

        // transform.Rotate();              // 오일러 만큼 추가 회전
        // transform.RotateAround();        // 특정 축 기준으로 회전
        // transform.LookAt();              // 특정 지점을 바라보게 만들기
    }
}
