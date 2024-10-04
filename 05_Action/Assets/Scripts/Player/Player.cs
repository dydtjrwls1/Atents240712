using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputController), typeof(PlayerMovement), typeof(PlayerAttack))]
public class Player : MonoBehaviour
{
    CharacterController m_CharacterController;

    PlayerInputController m_PlayerInputController;

    PlayerMovement m_PlayerMovement;

    PlayerAttack m_PlayerAttack;

    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();

        m_PlayerInputController = GetComponent<PlayerInputController>();
        m_PlayerInputController.onMove += OnMoveInput;
        m_PlayerInputController.onMoveModeChange += OnMoveModeChange;

        m_PlayerMovement = GetComponent<PlayerMovement>();
        m_PlayerInputController.onMove += m_PlayerMovement.SetDirection;
        m_PlayerInputController.onMoveModeChange += m_PlayerMovement.ToggleMoveMode;

        m_PlayerAttack = GetComponent<PlayerAttack>();
        m_PlayerInputController.onAttack += m_PlayerAttack.OnAttackInput;
    }

    // Update is called once per frame
    void Update()
    {
        // m_CharacterController.Move(Time.deltaTime * inputDirection); // 수동 움직임
        // characterController.SimpleMove(inputDirection); // 자동 움직임
    }

    // 이동 입력에 대한 델리게이트로 실행되는 함수 (이동방향, 이동상황)
    private void OnMoveInput(Vector2 input, bool isPress)
    {
        // 카메라 기준으로 앞뒤좌우로 이동한다.
    }

    private void OnMoveModeChange()
    {
    }
}
