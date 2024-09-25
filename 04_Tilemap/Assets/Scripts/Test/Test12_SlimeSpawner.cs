using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test12_SlimeSpawner : TestBase
{
    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        Slime[] slimes = FindObjectsByType<Slime>(FindObjectsSortMode.None);
        foreach(Slime slime in slimes) { slime.Die(); }
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        Slime[] slimes = FindObjectsByType<Slime>(FindObjectsSortMode.None);
        foreach (Slime slime in slimes) { slime.ShowPath(true); }
    }
}
