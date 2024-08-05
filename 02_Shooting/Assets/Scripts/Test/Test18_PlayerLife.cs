using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test18_PlayerLife : TestBase
{
    public Transform powerUpTarget;

    public PowerUp[] powerUps;

    Player player;

    private void Start()
    {
        for(int i = 0; i < powerUps.Length; i++)
        {
            powerUps[i].transform.position = powerUpTarget.position + Vector3.right * i;
        }

        player = GameManager.Instance.Player;
    }

    // 테스트용 코드
#if UNITY_EDITOR
    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        player.TestLifeUp();
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        player.TestLifeDown();
    }

    protected override void Test3_performed(InputAction.CallbackContext context)
    {
        player.TestDeath();
    }
#endif
}
