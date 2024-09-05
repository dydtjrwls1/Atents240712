using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test15_PlayerDie : TestBase
{
    Player player;

    private void Start()
    {
        player = GameManager.Instance.Player;
    }

    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        player.Die();
        Time.timeScale = 0.1f;
    }
}
