using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // InputAction 초기화
    PlayerInputActions inputActions;

    // 키보드로 받는 input 값
    float input;

    // 플레이어의 속도
    public float speed = 10.0f;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.XYMove.performed += XYMove;
        inputActions.Player.XYMove.canceled += XYMoveOff;

    }

    private void XYMoveOff(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        input = 0.0f;
    }

    private void XYMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        input = context.ReadValue<float>();
    }

    private void OnDisable()
    {
        inputActions.Player.XYMove.canceled -= XYMoveOff;
        inputActions.Player.XYMove.performed -= XYMove;
        inputActions.Disable();
    }



    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * speed * input * Vector2.right);
    }
}
