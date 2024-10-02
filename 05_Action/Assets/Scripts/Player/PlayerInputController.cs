using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    PlayerInputActions inputActions;

    // 이동 입력을 전달하는 델리게이트 (이동방향, 이동상황)
    public Action<Vector2, bool> onMove = null;

    // 이동 모드 변경 입력을 알리는 델리게이트
    public Action onMoveModeChange = null;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.MoveModeChange.performed += OnMoveModeChange;
    }

    private void OnDisable()
    {
        inputActions.Player.MoveModeChange.performed -= OnMoveModeChange;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Disable();
    }

    private void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        onMove?.Invoke(direction, !context.canceled);
    }

    private void OnMoveModeChange(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        onMoveModeChange?.Invoke();
    }
}