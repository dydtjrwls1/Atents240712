using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test08_ConsumeItem : Test07_ItemDrop
{
    PlayerStatus status;

    protected override void Awake()
    {
        base.Awake();
        item = ItemCode.SilverCoin;
    }

    protected override void Start()
    {
        base.Start();
        status = player.GetComponent<PlayerStatus>();
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        status.HealthHeal(-50.0f);
    }

    protected override void Test3_performed(InputAction.CallbackContext context)
    {
        status.ManaHeal(-90.0f);
    }
}
