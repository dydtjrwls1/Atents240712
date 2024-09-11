using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 이동 속도
    public float speed = 3.0f;
    public float attackCoolDown = 1.0f;

    // 입력받은 방향
    Vector2 inputDirection = Vector2.zero;

    // 인풋 액션
    PlayerInputActions inputActions;

    // 필요 컴포넌트
    Rigidbody2D rigid;
    Animator animator;

    float remainsAttackCoolDown;

    bool CanAttack => remainsAttackCoolDown < 0;

    // 애니메이터 해쉬값
    readonly int InputX_Hash = Animator.StringToHash("InputX");
    readonly int InputY_Hash = Animator.StringToHash("InputY");
    readonly int IsMove_Hash = Animator.StringToHash("IsMove");
    readonly int Attack_Hash = Animator.StringToHash("Attack");

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnStop;
        inputActions.Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        inputActions.Player.Attack.performed -= OnAttack;
        inputActions.Player.Move.canceled -= OnStop;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Disable();
    }

    private void Update()
    {
        remainsAttackCoolDown -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + Time.fixedDeltaTime * speed * inputDirection);
    }

    private void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();                               // 입력 받은 방향 저장

        animator.SetFloat(InputX_Hash, inputDirection.x);                            // 방향 애니메이터에 전달
        animator.SetFloat(InputY_Hash, inputDirection.y);
        animator.SetBool(IsMove_Hash, true);                                         // 애니메이터에 이동 시작 알림
    }

    private void OnStop(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        inputDirection = Vector2.zero;                                               // 이동 방향 초기화

        // x,y 세팅 안함 (이동 방향을 계속 바라봐게 하기 위해)
        animator.SetBool(IsMove_Hash, false);                                        // 애니메이터에 이동 정지 알림
    }

    private void OnAttack(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(CanAttack)
        {
            Attack();
        }
        
    }

    private void Attack()
    {
        remainsAttackCoolDown = attackCoolDown;
        animator.SetTrigger(Attack_Hash);                                             // 애니메이터에 공격 알림
    }
}
