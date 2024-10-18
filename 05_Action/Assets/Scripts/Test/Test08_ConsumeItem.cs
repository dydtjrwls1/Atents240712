using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test08_ConsumeItem : Test07_ItemDrop
{
    protected override void Awake()
    {
        base.Awake();
        item = ItemCode.SilverCoin;
    }
}
