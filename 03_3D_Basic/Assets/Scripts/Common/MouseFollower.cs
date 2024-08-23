using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    TestInputAction inputAction;
    private void Awake()
    {
        inputAction = new TestInputAction();
    }


    private void OnEnable()
    {
        inputAction.Test.Enable();
        inputAction.Test.PointerMouse.performed += OnPointerMove;
    }

    private void OnDisable()
    {
        inputAction.Test.PointerMouse.performed -= OnPointerMove;
        inputAction.Test.Enable();
    }



    private void OnPointerMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        
        Vector3 mousePos = context.ReadValue<Vector2>();                        // 마우스 스크린 좌표
        Debug.Log(mousePos);
        mousePos.z = transform.position.z - Camera.main.transform.position.z;   // 카메라 위치만큼 떨어뜨리기

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);            // 스크린 좌표 월드 좌표로 변경
        transform.position = worldPos;
    }
}
