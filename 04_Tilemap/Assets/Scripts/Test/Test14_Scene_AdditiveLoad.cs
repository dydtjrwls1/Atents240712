using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Test14_Scene_AdditiveLoad : TestBase
{
    [Range(0, 2)]
    public int targetX = 0;

    [Range(0, 2)]
    public int targetY = 0;

    protected override void Test1_performed(InputAction.CallbackContext _)
    {
        SceneManager.LoadScene($"Seemless_{targetX}_{targetY}", LoadSceneMode.Additive);
    }

    protected override void Test2_performed(InputAction.CallbackContext _)
    {
        SceneManager.UnloadSceneAsync($"Seemless_{targetX}_{targetY}");
    }
}
