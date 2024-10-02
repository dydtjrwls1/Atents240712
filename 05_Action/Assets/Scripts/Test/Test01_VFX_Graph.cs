using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class Test01_VFX_Graph : TestBase
{
    public VisualEffect effect;

    public float duration = 3.0f;

    readonly int OnStartEventID = Shader.PropertyToID("OnStart");
    readonly int DurationID = Shader.PropertyToID("Duration");

    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        effect.SendEvent(OnStartEventID);
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        effect.SetFloat(DurationID, duration);
    }
}
