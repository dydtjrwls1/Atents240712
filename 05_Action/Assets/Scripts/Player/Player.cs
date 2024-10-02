using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController characterController;

    PlayerInputController inputController;

    // 입력된 이동방향
    Vector3 inputDirection = Vector3.zero;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        inputController = GetComponent<PlayerInputController>();
        inputController.onMove += OnMoveInput;
        inputController.onMoveModeChange += OnMoveModeChange;
    }

    // Update is called once per frame
    void Update()
    {
        characterController.Move(Time.deltaTime * inputDirection); // 수동 움직임
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
