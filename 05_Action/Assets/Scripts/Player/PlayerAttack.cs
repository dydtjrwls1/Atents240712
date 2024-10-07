using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAttack : MonoBehaviour
{
    // 애니메이션 재생 시간
    const float AttackAnimationLength = 0.533f;

    // 쿨타임 설정용 변수(콤보를 위해서 애니메이션 재생 시간보다 작아야한다.)
    [Range(0, AttackAnimationLength)]
    public float maxCoolTime = 0.3f;

    // 현재 남아있는 쿨타임
    float coolTime = 0.0f;

    PlayerMovement m_PlayerMovement;
    Animator m_Animator;

    readonly int Attack_Hash = Animator.StringToHash("Attack");

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_PlayerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        coolTime -= Time.deltaTime;    
    }

    // 공격 입력이 들어오면 실행되는 함수
    public void OnAttackInput()
    {
        Attack();
    }

    // 공격 한번을 하는 함수
    void Attack()
    {
        // 쿨타임 시간이 모두 지나고 달릴 때는 공격하지 않는다
        if(coolTime < 0 && m_PlayerMovement.MoveMode != PlayerMovement.MoveState.Run)
        {
            m_Animator.SetTrigger(Attack_Hash);
            coolTime = maxCoolTime;
        }
    }
}
