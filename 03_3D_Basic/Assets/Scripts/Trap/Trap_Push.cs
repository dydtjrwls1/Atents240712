using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Push : TrapBase
{
    // 바닥에 안보이던 판이 일어나면서 플레이어를 밀어낸다.
    public float pushPower = 10.0f;

    Animator animator;

    readonly int Activate_Hash = Animator.StringToHash("Activate");
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void OnTrapActivate(GameObject target)
    {
        animator.SetTrigger(Activate_Hash);
        Rigidbody playerRb = target.GetComponent<Rigidbody>();
        Vector3 pushDirection = -target.transform.forward + target.transform.up; // 플레이어의 접근 위치에서 반대 대각선 방향
        playerRb?.AddForce(pushDirection * pushPower, ForceMode.Impulse);
    }
}
