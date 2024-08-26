using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Spike : TrapBase
{
    Animator animator;

    readonly int Activate_Hash = Animator.StringToHash("Activate");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // 밟으면 아래쪽에서 가시가 솟아오르고 그 가시에 닿으면 플레이어 사망
    protected override void OnTrapActivate(GameObject target)
    {
        animator.SetTrigger(Activate_Hash);   
    }
}
