using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // InputAction 초기화를 위한 변수
    PlayerInputActions inputActions;

    // 키보드로 받는 input 값 (-1.0 or 1.0)
    float input;

    // 플레이어의 속도
    public float speed = 10.0f;

    // player 오브젝트의 SpriteRenderer 저장을 위한 변수
    SpriteRenderer sr;

    // 피격 시 깜빡이는 시간간격을 위한 waitforseconds
    WaitForSeconds blinkIntervalWait;

    // 피격 시 깜빡이는 시간간격
    float blinkInterval = 0.1f;

    // 원래 색상
    Color orgColor;

    // 피격 시 바뀌는 색상
    Color hitColor;

    // 피격 시 투명해지는 정도
    float transperancy = 0.1f;

    // 목숨 변수
    int life = 5;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        sr = GetComponent<SpriteRenderer>();

        // 여러번 new 호출하는걸 방지하기 위한 waitforseconds
        blinkIntervalWait = new WaitForSeconds(blinkInterval);

        // 피격 시 변화할 색상 설정
        hitColor = Color.red;
        hitColor.a = transperancy;

        // 원본 색상 저장
        orgColor = sr.material.color;
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.XYMove.performed += XYMove;
        inputActions.Player.XYMove.canceled += XYMoveOff;

    }
    private void OnDisable()
    {
        inputActions.Player.XYMove.canceled -= XYMoveOff;
        inputActions.Player.XYMove.performed -= XYMove;
        inputActions.Disable();
    }

    private void XYMoveOff(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        input = 0.0f;
    }

    private void XYMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        input = context.ReadValue<float>();
        if (input < 0.0f)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 캐릭터 이동
        transform.Translate(Time.deltaTime * speed * input * Vector2.right);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        life--;
        Debug.Log($"Life : {life}");
        // 피격 시 HitState 코루틴 실행
        StartCoroutine(HitBlink());

        if (life == 0) { Time.timeScale = 0.0f; }
    }

    // 캐릭터가 두번 점멸하는 코루틴
    IEnumerator HitBlink()
    {
        sr.material.color = hitColor;
        yield return blinkIntervalWait;

        sr.material.color = orgColor;
        yield return blinkIntervalWait;

        sr.material.color = hitColor;
        yield return blinkIntervalWait;

        sr.material.color = orgColor;
        yield return blinkIntervalWait;
    }
}
