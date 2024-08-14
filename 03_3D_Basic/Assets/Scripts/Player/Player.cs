using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    public float rotateSpeed = 180.0f;

    PlayerInputActions inputActions;

    // 회전방향(음수면 좌회전, 양수면 우회전)
    private float rotateDirection = 0.0f;
    // 이동방향(음수면 후진, 양수면 전진)
    private float moveDirection = 0.0f;

    Rigidbody rb;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += On_MoveInput;
        inputActions.Player.Move.canceled += On_MoveInput;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.canceled -= On_MoveInput;
        inputActions.Player.Move.performed -= On_MoveInput;
        inputActions.Player.Disable();
    }

    private void FixedUpdate()
    {
        Movement(Time.fixedDeltaTime);
    }

    /// <summary>
    /// 이동 및 회전처리
    /// </summary>
    private void Movement(float deltaTime)
    {
        // 새 이동할 위치 : 현재위치 + 초당 moveSpeed의 속도로, 오브젝트의 앞 쪽 방향을 기준으로 전진/후진/정지
        Vector3 position = rb.position + deltaTime * moveSpeed * moveDirection * transform.forward;

        // 새 회전 : 현재회전 + 초당 rotateSpeed의 속도로, 좌회전/우회전/정지하는 회전
        Quaternion rotation = rb.rotation * Quaternion.AngleAxis(deltaTime * rotateSpeed * rotateDirection, transform.up);

        rb.Move(position, rotation);
    }

    private void On_MoveInput(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        SetInput(context.ReadValue<Vector2>(), !context.canceled);
    }

    /// <summary>
    /// 이동 입력 처리용 함수
    /// </summary>
    /// <param name="input">입력된 방향</param>
    /// <param name="isMove">이동 중이면 true, 아니면 false</param>
    void SetInput(Vector2 input, bool isMove)
    {
        rotateDirection = input.x;
        moveDirection = input.y;
    }
}
