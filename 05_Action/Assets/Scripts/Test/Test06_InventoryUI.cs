using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test06_InventoryUI : TestBase
{
    public InventoryUI inventoryUI;
    Inventory inven;

    public ItemCode item;
    public uint from;


#if UNITY_EDITOR
    private void Start()
    {
        inven = new Inventory(null);
        inventoryUI.InitializeInventory(inven);
        inven.AddItem(ItemCode.Ruby);
        inven.AddItem(ItemCode.Sapphire);
        inven.AddItem(ItemCode.Sapphire);
        inven.AddItem(ItemCode.Emerald);
        inven.AddItem(ItemCode.Emerald);
        inven.AddItem(ItemCode.Emerald);
        inven.AddItem(ItemCode.Emerald);
        inven.AddItem(ItemCode.Emerald);
        inven.AddItem(ItemCode.Emerald);
        inven.MoveItem(2, 3);
    }

    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        inven.AddItem(item, from);
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        inven.RemoveItem(from);
    }

    protected override void Test3_performed(InputAction.CallbackContext context)
    {
        inven.ClearSlot(from);
    }

    protected override void Test4_performed(InputAction.CallbackContext context)
    {
        inven.ClearInventory();
    }
#endif
}
