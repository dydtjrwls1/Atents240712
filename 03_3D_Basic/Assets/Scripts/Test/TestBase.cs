using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Random = UnityEngine.Random;      // 다른 클래스에서 제공하는 Random 이 있어도 UnityEngine의 Random 사용.

public class TestBase : MonoBehaviour
{
    const int AllRandom = -1;
    public int seed = AllRandom; // public 으로 되어 있는 변수는 인스펙터창에서 확인이 가능하다.

    //[SerializeField] // SerializeField attribute 가 있는 변수도 인스펙터 창에서 확인 가능하다. (유니티 쪽은 Public을 권장하고 있음.)
    //int ssss = 1;

    // 테스트용 인풋액션을 저장할 멤버 변수
    protected TestInputAction inputActions;

    IEnumerator test;
    private void Awake() // 스크립트가 생성되면 실행되는 함수
    {
        inputActions = new TestInputAction(); // Testinputaction 을 새로 생성.

        if( seed != AllRandom )
        {
            Random.InitState( seed );
        }

        test = Test();
    }

    private void OnEnable() // 이 스크립트가 활성화 될 때마다 실행되는 함수
    {
        inputActions.Test.Enable(); // Test 액션맵 활성화 하기
        inputActions.Test.Test1.performed += Test1_performed; // 액션과 함수 바인딩
        inputActions.Test.Test2.performed += Test2_performed;
        inputActions.Test.Test3.performed += Test3_performed;
        inputActions.Test.Test4.performed += Test4_performed;
        inputActions.Test.Test5.performed += Test5_performed;
        inputActions.Test.LClick.performed += LClick_performed;
        inputActions.Test.RClick.performed += RClick_performed;
        inputActions.Test.TestWASD.performed += TestWASD_performed;
        inputActions.Test.TestWASD.canceled += TestWASD_canceled;
    }

    protected virtual void TestWASD_performed(InputAction.CallbackContext context)
    {
        
    }

    protected virtual void TestWASD_canceled(InputAction.CallbackContext context)
    {

    }

    protected virtual void Test1_performed(InputAction.CallbackContext context)
    {
        Debug.Log("부모 Test1");
    }

    protected virtual void Test2_performed(InputAction.CallbackContext context)
    {
    }

    protected virtual void Test3_performed(InputAction.CallbackContext context)
    {
    }

    protected virtual void Test4_performed(InputAction.CallbackContext context)
    {

    }

    protected virtual void Test5_performed(InputAction.CallbackContext context)
    {

    }

    protected virtual void LClick_performed(InputAction.CallbackContext context)
    {

    }

    protected virtual void RClick_performed(InputAction.CallbackContext context)
    {

    }

    private void OnDisable()
    {
        inputActions.Test.TestWASD.canceled -= TestWASD_canceled;
        inputActions.Test.TestWASD.performed -= TestWASD_performed;
        inputActions.Test.RClick.performed -= RClick_performed;
        inputActions.Test.LClick.performed -= LClick_performed;
        inputActions.Test.Test5.performed -= Test5_performed;
        inputActions.Test.Test4.performed -= Test4_performed;
        inputActions.Test.Test3.performed -= Test3_performed;
        inputActions.Test.Test2.performed -= Test2_performed;
        inputActions.Test.Test1.performed -= Test1_performed;
        inputActions.Test.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Test()
    {
        while (true)
        {
            Debug.Log("Test");
            yield return new WaitForSeconds(1.0f);
        }
    }
}
