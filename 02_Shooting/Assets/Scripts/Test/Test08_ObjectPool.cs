using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test08_ObjectPool : TestBase
{
    public BulletPool bulletPool;

    // Start is called before the first frame update
    void Start()
    {
        bulletPool.Initialize();
    }

    protected override void Test1_performed(InputAction.CallbackContext _)
    {
        bulletPool.GetObject();
    }
}
