using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 이동 속도
    public float speed = 3.0f;

    // 공격 쿨타임
    public float attackCoolDown = 1.0f;

    // 플레이어 최대 수명
    public float maxLifeTime = 100.0f;

    // 입력받은 방향
    Vector2 inputDirection = Vector2.zero;

    // 인풋 액션
    PlayerInputActions inputActions;

    // 필요 컴포넌트
    Rigidbody2D rigid;
    Animator animator;
    // AttackSensor 의 축
    Transform attackSensorAxis;
    
    // 현재 공격 범위 내에 있는 모든 slime의 목록
    List<Slime> attackTargetList;

    // 플레이어가 살아있는지 표시하는 변수
    bool isAlive = true;

    // 지금 공격이 유효한 상태인지 확인하는 변수
    bool isAttackValid = false;

    // 남은 공격 쿨타임
    float remainsAttackCoolDown = 0.0f;

    // 플레이어 현재 수명
    float lifeTime;

    // 현재 속도
    float currentSpeed = 3.0f;

    float playTime = 0.0f;

    public float PlayTime => playTime;

    public float MaxLifeTime => maxLifeTime;

    float LifeTime
    {
        get => lifeTime;
        set
        {
            lifeTime = value;
            if(isAlive && lifeTime < 0.0f)
            {
                // 플레이어 사망
                Die();
            }
            else
            {
                lifeTime = Mathf.Clamp(lifeTime, 0.0f, maxLifeTime);
                onLifeTimeChange?.Invoke(lifeTime / MaxLifeTime);
            }
        }
    }

    // 플레이어가 죽인 슬라임의 수
    int killCount = -1;

    // 킬 카운트를 확인하고 설정하는 프로퍼티
    public int KillCount
    {
        get => killCount;
        set
        {
            if(killCount != value)
            {
                killCount = value;
                onKillCountChange?.Invoke(killCount);   // 값의 변경이 있을 때만 알림
            }
        }
    }

    // 공격 가능 여부 프로퍼티 ( 쿨타임이 다 되면 true, 안 됐으면 false )
    bool IsAttackReady => remainsAttackCoolDown < 0;

    // 애니메이터 해쉬값
    readonly int InputX_Hash = Animator.StringToHash("InputX");
    readonly int InputY_Hash = Animator.StringToHash("InputY");
    readonly int IsMove_Hash = Animator.StringToHash("IsMove");
    readonly int Attack_Hash = Animator.StringToHash("Attack");

    public Action<Vector3> onMove = null;
    // 플레이어의 수명이 변경되었을 경우 실행될 델리게이트(float : 현재 수명 / 최대 수명)
    public Action<float> onLifeTimeChange = null;
    public Action onDie = null;

    // 플레이어가 킬을 할 때마다 실행되는 델리게이트
    public Action<int> onKillCountChange = null;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        attackSensorAxis = transform.GetChild(0);
        AttackSensor sensor = attackSensorAxis.GetComponentInChildren<AttackSensor>();
        sensor.onSlimeEnter += (slime) =>
        {
            if (isAttackValid) // 공격이 유효할 때 범위 내에 들어올 경우 바로 사망
            {
                EnemyKill(slime.LifeTimeBonus);
                slime.Die();
            }
            else
            {
                attackTargetList.Add(slime); // 공격이 유효하지 않으면 일단 List에 추가
            }
            slime.ShowOutline(true);
        };
        sensor.onSlimeExit += (slime) =>
        {
            slime.ShowOutline(false);
            attackTargetList.Remove(slime);
        };

        attackTargetList = new List<Slime>(4);
        currentSpeed = speed;
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

    private void Start()
    {
        KillCount = 0;
        LifeTime = MaxLifeTime;    
    }

    private void Update()
    {
        remainsAttackCoolDown -= Time.deltaTime;
        LifeTime -= Time.deltaTime;
        if (isAlive)
        {
            playTime += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + Time.fixedDeltaTime * currentSpeed * inputDirection);
        onMove?.Invoke(transform.position);
    }

    private void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();                               // 입력 받은 방향 저장
        AttackSensorRotate(inputDirection);

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
        if(IsAttackReady)
        {
            Attack();
        }
    }

    private void Attack()
    {
        animator.SetTrigger(Attack_Hash);                                             // 애니메이터에 공격 알림
        remainsAttackCoolDown = attackCoolDown;                                       // 공격 쿨타임 초기화
        currentSpeed = 0.0f;
        
    }

    public void RestoreSpeed()
    {
        currentSpeed = speed;
    }

    /// <summary>
    /// 입력 방향에 따라 AttackSensor를 회전시키는 함수
    /// </summary>
    /// <param name="direction"></param>
    void AttackSensorRotate(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.01f) return;
        attackSensorAxis.up = -direction;
    }

    /// <summary>
    /// 공격 애니메이션 진행 중에 공격이 유효해지면 애니메이션 이벤트로 실행할 함수
    /// </summary>
    void AttackValid()
    {
        isAttackValid = true;
        foreach(var slime in attackTargetList)
        {
            EnemyKill(slime.LifeTimeBonus);
            slime.Die();
        }
        attackTargetList.Clear();
    }

    void AttackNotValid()
    {
        isAttackValid = false;
    }

    // 플레이어 사망처리 함수
    private void Die()
    {
        isAlive = false;
        LifeTime = 0.0f;
        onLifeTimeChange?.Invoke(0.0f);
        inputActions.Player.Disable();
        onDie?.Invoke();
    }

    /// <summary>
    /// 적을 죽였을 때 실행할 함수
    /// </summary>
    /// <param name="bonus">적 보너스(남은 시간 증가)</param>
    void EnemyKill(float bonus)
    {
        lifeTime += bonus;
        KillCount++;
    }
}
