using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test08_ConsumeItem : Test07_ItemDrop
{
    PlayerStatus playerStatus;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        item = ItemCode.Food;
        playerStatus = player.GetComponent<PlayerStatus>();
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        playerStatus.HP = 1;
        playerStatus.MP = 0;
    }
}
