using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 5.0f;
    public float walkSpeed = 3.0f;
    
    public float currentSpeed = 1.0f;
    public float turnSmooth = 10.0f;

    CharacterController m_CharacterController;

    Animator m_Animator;

    // 이동방향
    Vector3 m_Direction = Vector3.zero;

    // 이동방향을 바라보는 회전
    Quaternion m_MoveRotation;

    MoveState m_CurrentMoveMode = MoveState.Run;

    // 애니메이터용 해시값 및 상수
    readonly int Speed_Hash = Animator.StringToHash("Speed");
    const float Animator_StopSpeed = 0f;
    const float Animator_WalkSpeed = 0.3f;
    const float Animator_RunSpeed = 1f;


    // 이동방향 확인 및 설정 프로퍼티
    public Vector3 Direction
    {
        get => m_Direction;
        set
        {
            m_Direction = value;
        }
    }

    // 모드 표시용 enum
    enum MoveState : byte
    {
        Stop,
        Walk,
        Run,
    }

    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        SetMoveSpeedAndAnimation(MoveState.Stop);
    }

    private void Update()
    {
        m_CharacterController.Move(Time.deltaTime * currentSpeed * m_Direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, m_MoveRotation, turnSmooth * Time.deltaTime);
    }

    // 방향키가 눌렸을 때 캐릭터의 이동방향을 정하는 함수
    public void SetDirection(Vector2 input, bool isPress)
    {
        Vector3 direction = new Vector3(input.x, 0f, input.y);

        if (isPress)
        {
            Quaternion camY = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0); // 카메라의 Y축 회전만 추출

            direction = camY * direction; // 카메라의 Y축 회전만큼 회전한 방향에서 입력방향만큼 회전시키기

            m_MoveRotation = Quaternion.LookRotation(direction);

            SetMoveSpeedAndAnimation(m_CurrentMoveMode);
        }
        else
        {
            SetMoveSpeedAndAnimation(MoveState.Stop);
        }

        direction = direction.normalized;
        Debug.Log($"Press {isPress}, direction : {direction}");
        m_Direction = direction;
    }

    public void ToggleMoveMode()
    {
        switch (m_CurrentMoveMode)
        {
            case MoveState.Walk:
                m_CurrentMoveMode = MoveState.Run;
                SetMoveSpeedAndAnimation(m_CurrentMoveMode);
                break;
            case MoveState.Run:
                m_CurrentMoveMode = MoveState.Walk;
                SetMoveSpeedAndAnimation(m_CurrentMoveMode);
                break;
        }
    }
    void SetMoveSpeedAndAnimation(MoveState mode)
    {
        switch (mode)
        {
            case MoveState.Stop:
                currentSpeed = 0.0f;
                m_Animator.SetFloat(Speed_Hash, Animator_StopSpeed);
                break;
            case MoveState.Walk:
                if(m_Direction.sqrMagnitude > 0)
                {
                    m_Animator.SetFloat(Speed_Hash, Animator_WalkSpeed);
                }
                currentSpeed = walkSpeed;
                break;
            case MoveState.Run:
                if (m_Direction.sqrMagnitude > 0)
                {
                    m_Animator.SetFloat(Speed_Hash, Animator_RunSpeed);
                }
                currentSpeed = runSpeed;
                break;
        }
    
    }
}