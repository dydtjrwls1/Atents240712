using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test05_Inventory2 : TestBase
{
    public ItemSortCriteria sortCriteria;
    public bool isAscending = true;

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
        inven.SlotSorting(sortCriteria, isAscending);
        inven.Test_InventoryPrint();
    }
#endif
}
