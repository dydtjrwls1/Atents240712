//#define PrintTestLog

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.Rendering;
using UnityEngine;


public class InvenSlot
{
    // 들어있는 아이템의 종류 null 이면 비어있다,
    ItemData m_SlotItemData = null;

    // 인벤토리에서 몇 번째 슬롯인지 나타내는 변수
    uint m_SlotIndex;

    // 아이템 개수
    uint m_ItemCount = 0;

    // 아이템 장착 여부 (true 면 장비중, false 면 장비하고 있지 않음)
    bool m_IsEquipped = false;

    // 슬롯에 들어있는 아이템의 종류를 확인하거나 쓰기 위한 프로퍼티 (쓰기는 private)
    public ItemData ItemData
    {
        get => m_SlotItemData;
        private set
        {
            if(m_SlotItemData != value)
            {
                m_SlotItemData = value;
                onSlotItemChange?.Invoke();
            }
        }
    }

    // 슬롯이 비었는지 확인하는 프로퍼티
    public bool IsEmpty =>  m_SlotItemData == null;

    // 아이템 개수를 확인하고 변경하는 프로퍼티
    public uint ItemCount
    {
        get => m_ItemCount;
        set
        {
            if(m_ItemCount != value)
            {
                m_ItemCount = value;
                onSlotItemChange?.Invoke();
            }
        }
    }

    // 슬롯의 인덱스를 확인하기 위한 프로퍼티
    public uint Index => m_SlotIndex;

    public bool IsEquipped
    {
        get => m_IsEquipped;
        set
        {
            m_IsEquipped = value;
            if(m_IsEquipped)
            {
                onItemEquip?.Invoke(this);
            }
            onSlotItemChange?.Invoke();
        }
    }

    // 슬롯의 아이템이 변경되었음을 알리는 델리게이트
    public event Action onSlotItemChange = null;

    // 아이템을 장비했음을 알리는 델리게이트 (InvenSlot : 장비한 아이템이 들어있는 슬롯)
    public event Action<InvenSlot> onItemEquip = null;

    public InvenSlot(uint index)
    {
        m_SlotIndex = index;
        ItemData = null;
        ItemCount = 0;
        IsEquipped = false;
    }

    // 아이템 할당, 제거

    /// <summary>
    /// 슬롯에 아이템을 할당하는 함수
    /// </summary>
    /// <param name="data">설정할 아이템</param>
    /// <param name="count">개수</param>
    /// <param name="isEquipped">설정할 장비 상태</param>
    public void AssignSlotItem(ItemData data, uint count = 1, bool isEquipped = false)
    {
        if(data != null)
        {
            ItemData = data;
            ItemCount = count;
            IsEquipped = isEquipped;

#if PrintTestLog
            Debug.Log($"인벤토리 [{m_SlotIndex}]번 슬롯에 [{ItemData.itemName}]아이템이 [{ItemCount}]개 설정.");
#endif
        }
        else
        {
            ClearSlotItem();
        }
    }

    /// <summary>
    /// 이 슬롯에 들어있는 아이템을 제거하는 함수
    /// </summary>
    public virtual void ClearSlotItem()
    {
        ItemData = null;
        ItemCount = 0;
        IsEquipped = false;

#if PrintTestLog
        Debug.Log($"슬롯을 비운다.");
#endif
    }

    // 아이템 개수 증가, 감소

    /// <summary>
    /// 이 슬롯에 들어있는 아이템 개수를 증가시키는 함수
    /// </summary>
    /// <param name="overCount">return이 false 가 됐을 때 남은 개수</param>
    /// <param name="increaseCount">증가시킬 개수</param>
    /// <returns>increaseCount 만큼 증가에 성공했으면 true, 남은 것이 있으면 false</returns>
    public bool IncreaseSlotItem(out uint overCount, uint increaseCount = 1)
    {
        bool result = false;

        uint newCount = ItemCount + increaseCount; // 합계 구하기
        int over = (int)newCount - (int)ItemData.maxStackCount; // 최대치를 넘었는지 계산하기.

#if PrintTestLog
        Debug.Log($"인벤토리 [{m_SlotIndex}]번 슬롯에 아이템 개수 증가 시도. 현재 [{ItemCount}]개.");
#endif

        if(over > 0)
        {
            // 넘쳤다
            ItemCount = ItemData.maxStackCount;
            overCount = (uint)over;

            result = false;

#if PrintTestLog
            Debug.Log($"슬롯이 넘침({over}개). 아이템이 최대치까지 증가");
#endif
        }
        else
        {
            // 안 넘쳤다
            ItemCount = newCount;
            overCount = 0;

            result = true;

#if PrintTestLog
            Debug.Log($"아이템이 [{increaseCount}]개 증가");
#endif
        }

        return result;
    }

    public void DecreaseSlotItem(uint decreaseCount = 1)
    {
        int newCount = (int)(ItemCount - decreaseCount);

        if(newCount > 0)
        {
            // 아직 아이템이 남아있음
            ItemCount = (uint)newCount;

#if PrintTestLog
            Debug.Log($"인벤토리 [{m_SlotIndex}]번 슬롯에 [{ItemData.itemName}]이 [{decreaseCount}]개 감소. 현재 [{ItemCount}]개.");
#endif
        }
        else
        {
            // 아이템이 더이상 남아있지 않음
            ClearSlotItem();
        }
    }

    // 아이템 장비

    /// <summary>
    /// 이 슬롯의 아이템을 장비하는 함수
    /// </summary>
    /// <param name="target">아이템을 장비할 대상</param>
    public void EquipItem(GameObject target)
    {

    }

    // 아이템 사용

    /// <summary>
    /// 이 슬롯의 아이템을 사용하는 함수
    /// </summary>
    /// <param name="target">아이템의 효과를 받을 대상</param>
    public void UseItem(GameObject target)
    {
        IUsable usable = ItemData as IUsable;
        if (usable != null)
        {
            usable.Use(target);
            DecreaseSlotItem();
        }
    }

    public void ClearDelegates()
    {
        onSlotItemChange = null;
        onItemEquip = null;
    }
}
