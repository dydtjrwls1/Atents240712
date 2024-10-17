using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IInitializable
{
    Inventory inventory;
    public Inventory Inventory => inventory;

    public float pickUpRange = 1.5f;

    int ItemLayerMask; 

    public void Initialize()
    {
        Player owner = GetComponent<Player>();
        inventory = new Inventory(owner);
        GameManager.Instance.InventoryUI.InitializeInventory(inventory);
        ItemLayerMask = 1 << LayerMask.NameToLayer("Item");
    }

    public void PickUpItems()
    {
        // 주변에 있는 아이템을 모두 획득해서 인벤토리에 추가하기
        Collider[] itemColliders = Physics.OverlapSphere(transform.position, pickUpRange, ItemLayerMask);

        if (itemColliders.Length != 0)
        {
            foreach(var item in itemColliders)
            {
                ItemObject obj = item.GetComponent<ItemObject>();

                if(obj != null)
                {
                    if (inventory.AddItem(obj.Data.code))
                    {
                        obj.CollectedItem(); // 아이템 추가 성공했으면 비활성화 하기
                    }
                }
            }
        }
    }
}
