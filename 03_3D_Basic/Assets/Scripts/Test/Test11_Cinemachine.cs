using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test11_Cinemachine : TestBase
{
    public CinemachineVirtualCamera[] vcams;

    CinemachineImpulseSource impulseSource;

    private void Start()
    {
        if (vcams.Length == 0)
        {
            vcams = FindObjectsByType<CinemachineVirtualCamera>(FindObjectsSortMode.None);
        }

        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        vcams[0].Priority = 100;
        vcams[1].Priority = 10;
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        vcams[1].Priority = 100;
        vcams[0].Priority = 10;
    }

    protected override void Test3_performed(InputAction.CallbackContext context)
    {
        impulseSource.GenerateImpulse();
    }
}
