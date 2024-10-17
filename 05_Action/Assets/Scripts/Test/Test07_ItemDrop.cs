using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test07_ItemDrop : TestBase
{
    public ItemCode item = ItemCode.Misc;
    public uint spawnCount = 1;
    public Transform spawnPosition;
    public bool useNoise = false;

    Player player;




#if UNITY_EDITOR
    private void Start()
    {
        player = GameManager.Instance.Player;
        player.Inventory.AddItem(ItemCode.Ruby);
        player.Inventory.AddItem(ItemCode.Ruby);

        player.Inventory.AddItem(ItemCode.Ruby);
        player.Inventory.AddItem(ItemCode.Sapphire);
        player.Inventory.AddItem(ItemCode.Sapphire);
        player.Inventory.AddItem(ItemCode.Emerald);
        player.Inventory.AddItem(ItemCode.Emerald);
        player.Inventory.AddItem(ItemCode.Emerald);
        player.Inventory.AddItem(ItemCode.Emerald);
        player.Inventory.AddItem(ItemCode.Emerald);
        player.Inventory.AddItem(ItemCode.Emerald);
        player.Inventory.MoveItem(2, 3);
    }

    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        Factory.Instance.MakeItems(item, spawnCount, spawnPosition.position, useNoise);
    }
#endif
}
