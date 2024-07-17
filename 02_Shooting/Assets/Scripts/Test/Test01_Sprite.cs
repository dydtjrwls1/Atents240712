using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test01_Sprite : MonoBehaviour
{
    
    TestInputAction inputActions;

    private void Awake() 
    {
        inputActions = new TestInputAction(); 

    }

    private void OnEnable()
    {
        
        inputActions.Test.Enable(); 
        //inputActions.Test.Test1.started += Test1_started; // 입력이 들어오면 발동
        inputActions.Test.Test1.performed += Test1_performed; // 입력이 충분히 들어오면 발동
        //inputActions.Test.Test1.canceled += Test1_canceled; // 입력을 취소하면 발동
    }

    protected virtual void Test1_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Performed");
    }

    protected virtual void Test1_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Canceled");
    }

    private void Test1_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Started");
    }

    private void OnDisable()
    {
        // 스크립트가 비활성화 될 때마다 실행되는 함수
        inputActions.Test.Disable();
        inputActions.Test.Test1.performed -= Test1_performed; // Test1.performed 에 등록되어 있던 함수 제거
    }

    // Start is called before the first frame update
    void Start()
    {
        // 첫번째 업데이트 함수가 실행되기 직전에 한번 호출되는 함수
        // Debug.Log("시작");
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A)) // pooling 방식. InputManager  방식
        //{
        //    Debug.Log("A 눌러짐");
        //}

                
    }
}
