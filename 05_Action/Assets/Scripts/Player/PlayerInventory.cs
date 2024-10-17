using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IInitializable
{
    Inventory inventory;
    public Inventory Inventory => inventory;

    public float range = 1.5f;

    int ItemLayerMask; 

    public void Initialize()
    {
        inventory = new Inventory(this);
        GameManager.Instance.InventoryUI.InitializeInventory(inventory);
        ItemLayerMask = 1 << LayerMask.NameToLayer("Item");
    }

    public void PickUpItems()
    {
        Collider[] items = Physics.OverlapSphere(transform.position, range, ItemLayerMask);

        // 주변에 있는 아이템을 모두 획득해서 인벤토리에 추가하기
        if (items.Length != 0)
        {
            foreach(var item in items)
            {
                ItemObject obj = item.GetComponent<ItemObject>();

                if(obj != null)
                {
                    inventory.AddItem(obj.Data.code);
                }
            }
        }
    }
}
