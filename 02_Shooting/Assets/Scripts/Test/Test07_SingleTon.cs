using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test07_SingleTon : TestBase
{
    private void Start()
    {
        // 객체 생성 방법
        // TestSingleTon test = new TestSingleTon(); // public 생성자가 없어서 호출 불가능
        TestClass test2 = new TestClass();
        TestClass test3 = new TestClass(5);

        TestClass.staticNumber = 10;
        test2.TestPrint();
        test3.TestPrint();
    }

    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        Debug.Log(SimpleFactory.Instance.gameObject);
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        SimpleFactory.Instance.Getbullet();
        SimpleFactory.Instance.GetEnemy(new Vector3(0, 5, 0), 45);
    }
}
