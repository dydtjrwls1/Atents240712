using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test10_EquipItem : Test07_ItemDrop
{
#if UNITY_EDITOR
    protected override void Start()
    {
        player = GameManager.Instance.Player;
        player.InventoryData.AddItem(ItemCode.GoldSword);
        player.InventoryData.AddItem(ItemCode.IronSword);
        player.InventoryData.AddItem(ItemCode.SilverSword);
        player.InventoryData.AddItem(ItemCode.Shield);
        player.InventoryData.AddItem(ItemCode.WoodShield);
    }
#endif
}
