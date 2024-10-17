using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    PlayerInputActions inputActions;

    // 이동 입력을 전달하는 델리게이트 (이동방향, 이동상황)
    public event Action<Vector2, bool> onMove = null;

    // 이동 모드 변경 입력을 알리는 델리게이트
    public event Action onMoveModeChange = null;

    // 공격 입력을 알리는 델리게이트
    public event Action onAttack = null;

    public event Action onPickUp = null;

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
        inputActions.Player.Attack.performed += OnAttack;
        inputActions.Player.PickUp.performed += OnPickUp;
    }

    

    private void OnDisable()
    {
        inputActions.Player.PickUp.performed -= OnPickUp;
        inputActions.Player.Attack.performed -= OnAttack;
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

    private void OnAttack(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        onAttack?.Invoke();
    }

    private void OnPickUp(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        onPickUp?.Invoke();
    }
}