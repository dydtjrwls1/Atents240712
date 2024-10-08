using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test05_Inventory : TestBase
{
    public ItemCode code = ItemCode.Misc;

    [Range(0, 5)]
    public uint from = 0;
    [Range(0, 5)]
    public uint to = 0;

    Inventory inven;

#if UNITY_EDITOR
    private void Start()
    {
        inven = new Inventory(null);
        inven.AddItem(ItemCode.Ruby);
        inven.AddItem(ItemCode.Sapphire);
        inven.AddItem(ItemCode.Sapphire);
        inven.AddItem(ItemCode.Emerald);
        inven.AddItem(ItemCode.Emerald);
        inven.AddItem(ItemCode.Emerald);
        inven.MoveItem(2, 3);
        inven.Test_InventoryPrint();
    }

    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        inven.SlotSorting(ItemSortCriteria.Code);
        inven.Test_InventoryPrint();
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        inven.SlotSorting(ItemSortCriteria.Name);
        inven.Test_InventoryPrint();
    }

    protected override void Test3_performed(InputAction.CallbackContext context)
    {
        inven.SlotSorting(ItemSortCriteria.Price);
        inven.Test_InventoryPrint();
    }

    protected override void Test4_performed(InputAction.CallbackContext context)
    {
        inven.ClearInventory();
        inven.Test_InventoryPrint();
    }

    protected override void Test5_performed(InputAction.CallbackContext context)
    {
        inven.MoveItem(from, to);
        inven.Test_InventoryPrint();
    }
#endif
}
