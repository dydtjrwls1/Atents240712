using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test15_SubmapManager : TestBase
{
    [Range(0, 2)]
    public int targetX = 0;

    [Range(0, 2)]
    public int targetY = 0;

    SubmapManager submapManager;

    private void Start()
    {
        submapManager = GameManager.Instance.SubmapManager;
    }

    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        submapManager.Test_LoadScene(targetX, targetY);
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        submapManager.Test_UnloadScene(targetX, targetY);
    }

    protected override void Test3_performed(InputAction.CallbackContext context)
    {
        submapManager.Test_UnloadAll();
    }

    protected override void Test4_performed(InputAction.CallbackContext context)
    {
        submapManager.Test_RefreshScenes(targetX, targetY);
    }
}