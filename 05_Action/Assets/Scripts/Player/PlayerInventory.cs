using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IInitializable, IMoneyContainer, IEquipTarget
{
    Inventory inventory;

    InvenSlot[] partsSlot;

    Transform weaponParent;

    Transform shieldParent;

    public float pickUpRange = 1.5f;

    int money = 0;

    int ItemLayerMask;

    public int Money
    {
        get => money;
        set
        {
            if(money != value)
            {
                money = value;
                onMoneyChange?.Invoke(money);
            }
        }
    }

    public Inventory Inventory => inventory;

    public InvenSlot this[EquipType equipType]
    {
        get => partsSlot[(int)equipType];
        set
        {
            partsSlot[(int)equipType] = value;
        }
    }
    public event Action<int> onMoneyChange = null;

    private void Awake()
    {
        Transform child = transform.GetChild(2); // root
        Transform spine3 = child.GetChild(0).GetChild(0).GetChild(0).GetChild(0); // spine 03

        child = spine3.GetChild(1); // clavicle_l
        child = child.GetChild(1); // upperarm_l
        shieldParent = child.GetChild(0).GetChild(0).GetChild(2);

        child = spine3.GetChild(2); // clavicle_l
        child = child.GetChild(1); // upperarm_l
        weaponParent = child.GetChild(0).GetChild(0).GetChild(2);

        partsSlot = new InvenSlot[Enum.GetValues(typeof(EquipType)).Length]; // EquipType 의 값들의 개수만큼 배열생성
    }

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
                    IConsumable consumable = obj.Data as IConsumable;

                    if (consumable != null)
                    {
                        consumable.Consume(gameObject);
                        obj.CollectedItem();
                    }
                    else
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

    public void EquipItem(EquipType part, InvenSlot slot)
    {
        ItemDataEquip equipItem = slot.ItemData as ItemDataEquip;
        if (equipItem != null)
        {
            Transform partParent = GetEquipParentTransform(part); // 장비를 위치시킬 트랜스폼
            GameObject obj = Instantiate(equipItem.equipPrefab, partParent);    // 장비 오브젝트 생성
            this[part] = slot;  // 장비가 있는 슬롯을 등록
            slot.IsEquipped = true; // 슬롯의 장비 장착 상태 갱신

            float power = 0f;
            switch (part)
            {
                case EquipType.Weapon:
                    ItemDataWeapon weapon = equipItem as ItemDataWeapon;
                    power = weapon.attackPower;
                    
                    break;
                case EquipType.Shield:
                    ItemDataShield shield = equipItem as ItemDataShield;
                    power = shield.defencePower;
                    break;
            }

            GameManager.Instance.Status.SetEquipPower(part, power); // 플레이어 스탯 변경
        }
    }

    public void UnEquipItem(EquipType equipType)
    {
        InvenSlot slot = partsSlot[(int)equipType];
        if(slot != null)
        {
            Transform partParent = GetEquipParentTransform(equipType);
            // 장비를 장착한 트랜스폼의 자식이 없을 때 까지 반복
            while(partParent.childCount > 0)
            {
                Transform child = partParent.GetChild(0);
                child.SetParent(null);
                Destroy(child.gameObject);
            }
            slot.IsEquipped = false;
            this[equipType] = null;

            GameManager.Instance.Status.SetEquipPower(equipType, 0);
        }
    }

    // 장비를 장착할 트랜스폼 반환하는 함수
    public Transform GetEquipParentTransform(EquipType part)
    {
        Transform result = null;

        switch (part)
        {
            case EquipType.Weapon:
                result = weaponParent;
                break;
            case EquipType.Shield:
                result = shieldParent;
                break;
        }

        return result;
    }
}
