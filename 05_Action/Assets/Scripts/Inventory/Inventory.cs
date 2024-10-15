//#define PrintTestLog
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SearchService;

public class Inventory
{
    // 인벤토리의 슬롯들
    InvenSlot[] slots;

    // 임시 슬롯(드래그나 아이템 분리작업에서 사용)
    InvenTempSlot m_InvenTempSlot;

    ItemDataManager m_ItemDataManager;

    // 인벤토리의 소유자
    Player m_Owner;

    const int Default_Inventory_Size = 6;

    // 현재 인벤토리 슬롯의 개수
    int SlotCount => slots.Length;

    // 소유자 확인용 프로퍼티
    public Player Owner => m_Owner;

    /// <summary>
    /// 인벤토리 슬롯에 접근하기 위한 인덱서
    /// </summary>
    /// <param name="index">슬롯의 인덱스</param>
    /// <returns>인덱스 번째의 슬롯</returns>
    public InvenSlot this[uint index] => slots[index];

    public InvenTempSlot TempSlot => m_InvenTempSlot;

    /// <summary>
    /// 인벤토리 클래스의 생성자
    /// </summary>
    /// <param name="owner">인벤토리의 소유자</param>
    /// <param name="size">인벤토리 크기</param>
    public Inventory(Player owner, uint size = Default_Inventory_Size)
    {
        slots = new InvenSlot[size];
        for(uint i = 0; i < slots.Length; i++)
        {
            slots[i] = new InvenSlot(i);
        }

        m_InvenTempSlot = new InvenTempSlot();
        m_ItemDataManager = GameManager.Instance.ItemData; // 타이밍 조심 필요

        m_Owner = owner;
    }

    // 아이템 이동
    // 아이템 스왑
    // 아이템 추가
    // 아이템 삭제
    // 아이템 덜어내서 임시슬롯에 저장
    // 인벤토리 정렬
    // 인벤토리 정리
    // 테스트 : 인벤토리 내용 출력




    public bool AddItem(ItemCode code, uint index)
    {
        bool result = false;

        // 적절한 인덱스인지 확인
        if(IsValidIndex(index, out InvenSlot slot))
        {
            // 인덱스가 적절할 경우
            ItemData data = m_ItemDataManager[code];

            // 슬롯이 비어있는지 확인
            if(slot.IsEmpty)
            {
                // 슬롯이 비어있는 경우
                slot.AssignSlotItem(data);
                result = true;
            }
            else
            {
                // 슬롯이 비어있지 않은 경우

                // 같은 종류의 아이템인지 확인
                if(slot.ItemData == data)
                {
                    // 같은 아이템인 경우
                    result = slot.IncreaseSlotItem(out _);
                }
                else
                {
                    // 다른 아이템인 경우
#if PrintTestLog
                    Debug.Log($"아이템 추가 실패 : [{index}] 번 슬롯에는 다른 아이템이 들어잇습니다.");
#endif
                }
            }

        }
        else
        {
            // 적절치 못한 인덱스일 경우
#if PrintTestLog
            Debug.Log($"아이템 추가 실패 : [{index}] 는 잘못된 인덱스다.");
#endif
        }

        return result;
    }

    public bool AddItem(ItemCode code)
    {
        bool result = false;

        for(uint i = 0; i < SlotCount; i++ )
        {
            if(AddItem(code, i))
            {
                result = true;
                break;
            }
        }

        return result;
    }

    /// <summary>
    /// 인벤토리의 특정 슬롯에서 아이템을 일정 개수만큼 제거하는 함수
    /// </summary>
    /// <param name="index">감소시킬 슬롯 인덱스</param>
    /// <param name="decreaseCount">감소할 수</param>
    public void RemoveItem(uint index, uint decreaseCount = 1)
    {
        if (IsValidIndex(index, out InvenSlot slot))
        {
            slot.DecreaseSlotItem(decreaseCount);
        }
        else
        {
#if PrintTestLog
            Debug.Log($"아이템 감소 실패 : [{index}] 는 잘못된 인덱스다.");
#endif
        }
    }

    /// <summary>
    /// 인벤토리의 특정 슬롯을 비우는 함수
    /// </summary>
    /// <param name="index">비울 슬롯의 인덱스</param>
    public void ClearSlot(uint index)
    {
        if(IsValidIndex(index, out InvenSlot slot))
        {
            slot.ClearSlotItem();
        }
        else
        {
#if PrintTestLog
            Debug.Log($"슬롯 비우기 실패 [{index}]는 잚ㅅ된 인덱스입니다.");
#endif
        }
    }

    /// <summary>
    /// 인벤토리 전체를 비우는 함수
    /// </summary>
    public void ClearInventory()
    {
        foreach(var slot in slots)
        {
            slot.ClearSlotItem();
        }
    }

    /// <summary>
    /// 인벤토리의 from 슬롯에 있는 아이템을 to 위치로 옮기는 함수
    /// </summary>
    public void MoveItem(uint from, uint to)
    {
        // from 과 to 가 서로 다르고 각각 적절한 슬롯이면
        if(from != to 
            && IsValidIndex(from, out InvenSlot fromSlot) 
            && IsValidIndex(to, out InvenSlot toSlot))
        {
            // from 에는 반드시 아이템이 들어있어야 한다.
            if (!fromSlot.IsEmpty)
            {
                //TempSlot.FromIndex = toSlot is InvenTempSlot ? from : null; // toSlot 이 임시 슬롯이면 FromIndex에 돌아갈 위치 설정
                if(toSlot is InvenTempSlot)
                {
                    TempSlot.FromIndex = from;
                }

                if (fromSlot.ItemData == toSlot.ItemData)
                {
#if PrintTestLog
                    Debug.Log($"아이템 이동 [{from}] 에서 [{to}]로 이동.");
#endif

                    // from 과 to 에 같은 아이템이 들어있는 경우
                    toSlot.IncreaseSlotItem(out uint overCount, fromSlot.ItemCount);
                    fromSlot.DecreaseSlotItem(fromSlot.ItemCount - overCount);
                }
                else
                {
                    // 다른 아이템이 들어있는 경우
                    if(fromSlot is InvenTempSlot)
                    {
                        // from 이 임시 슬롯이다(to 는 일반 슬롯이다)
                        // temp슬롯에서 to 슬롯으로 아이템을 옮기는 경우 (= 드래그가 끝나는 상황)
                        // (일반적인 아이템 이동 상황)
                        // 임시 슬롯에 있던 것이 to 로 들어가고 to 에 있던것이 drag 시작한 슬롯으로 돌아간다.
                        InvenSlot dragStartSlot = slots[TempSlot.FromIndex.Value];

                        if (dragStartSlot.IsEmpty)
                        {
                            // dragStartSlot 이 비어있다, (드래그로 아이템 옮기기)
                            dragStartSlot.AssignSlotItem(toSlot.ItemData, toSlot.ItemCount, toSlot.IsEquipped);
                            toSlot.AssignSlotItem(TempSlot.ItemData, TempSlot.ItemCount, TempSlot.IsEquipped);
                            TempSlot.ClearSlotItem();
                        }
                        else
                        {
                            // dragStartSlot 에 아이템이 있다. (시작 슬롯에서 아이템을 덜어낸 상황)
                            if (dragStartSlot.ItemData == toSlot.ItemData)
                            {
                                dragStartSlot.IncreaseSlotItem(out uint overCount, toSlot.ItemCount);
                                toSlot.DecreaseSlotItem(toSlot.ItemCount - overCount);
                            }
                            SwapSlot(TempSlot, toSlot);
                        }
                    }
                    else
                    {
                        // from 슬롯이 임시 슬롯이 아닌 경우
#if PrintTestLog
                        Debug.Log($"아이템 이동 [{from}] 슬롯과 [{to}]슬롯을 서로 스왑.");
#endif
                        SwapSlot(fromSlot, toSlot);
                    }
                }
            }
        }
    }

    // 인벤토리의 특정 슬롯에서 아이템을 일정량 덜어내어 임시 슬롯으로 보내는 함수.
    public void SplitItem(uint slotIndex, uint count)
    {
        InvenSlot fromSlot = slots[slotIndex];
        uint resultCount = fromSlot.ItemCount - count;

        fromSlot.ItemCount = resultCount;

        TempSlot.FromIndex = slotIndex;
        TempSlot.ItemCount = count;
    }

    /// <summary>
    /// 슬롯간에 스왑을 하는 함수
    /// </summary>
    void SwapSlot(InvenSlot slotA, InvenSlot slotB)
    {
        ItemData tempData = slotA.ItemData;
        uint tempCount = slotA.ItemCount;
        bool tempEquip = slotA.IsEquipped;
        slotA.AssignSlotItem(slotB.ItemData, slotB.ItemCount, slotB.IsEquipped);
        slotB.AssignSlotItem(tempData, tempCount, tempEquip);
    }

    /// <summary>
    /// 슬롯 인덱스가 적절한 인덱스인지 확인하는 함수
    /// </summary>
    /// <param name="index">확인할 인덱스 번호</param>
    /// <param name="targetSlot">index가 가리키는 슬롯</param>
    /// <returns>존재하는 인덱스면 true, 아니면 false</returns>
    bool IsValidIndex(uint index, out InvenSlot targetSlot)
    {
        targetSlot = null;

        if(index < SlotCount)
        {
            targetSlot = slots[index];
        }
        else if (index == InvenTempSlot.TempSlotIndex)
        {
            targetSlot = TempSlot;
        }

        return targetSlot != null;
    }

    /// <summary>
    /// 인벤토리를 정렬하는 함수
    /// </summary>
    /// <param name="sortCriteria">정렬 기준</param>
    /// <param name="isAscending">true 면 오름차순, false 면 내림차순</param>
    public void SlotSorting(ItemSortCriteria sortCriteria, bool isAscending = true)
    {
        List<InvenSlot> sortList = new List<InvenSlot>(slots); // 정렬용 리스트 만들기 (원본의 변형을 방지하지 위하여)

        switch (sortCriteria)
        {
            case ItemSortCriteria.Code:
                sortList.Sort((current, other) =>
                {
                    if (current.ItemData == null) // 빈 슬롯은 뒤로
                        return 1;
                    if (other.ItemData == null)
                        return -1;
                    if(isAscending)
                    {
                        return current.ItemData.code.CompareTo(other.ItemData.code);
                    }
                    else
                    {
                        return other.ItemData.code.CompareTo(current.ItemData.code);
                    }
                });
                break;
            case ItemSortCriteria.Name:
                sortList.Sort((current, other) =>
                {
                    if (current.ItemData == null) // 빈 슬롯은 뒤로
                        return 1;
                    if (other.ItemData == null)
                        return -1;
                    if (isAscending)
                    {
                        return current.ItemData.name.CompareTo(other.ItemData.name);
                    }
                    else
                    {
                        return other.ItemData.name.CompareTo(current.ItemData.name);
                    }
                });
                break;
            case ItemSortCriteria.Price:
                sortList.Sort((current, other) =>
                {
                    if (current.ItemData == null) // 빈 슬롯은 뒤로
                        return 1;
                    if (other.ItemData == null)
                        return -1;
                    if (isAscending)
                    {
                        return current.ItemData.price.CompareTo(other.ItemData.price);
                    }
                    else
                    {
                        return other.ItemData.price.CompareTo(current.ItemData.price);
                    }
                });
                break;
        }

        // 정렬된 데이터만 저장하는 리스트 생성 (직접 sortList 사용 시 데이터가 섞이기 때문)
        List<(ItemData, uint, bool)> sortedData = new List<(ItemData, uint, bool)>(SlotCount);

        foreach(var slot in sortList)
        {
            sortedData.Add((slot.ItemData, slot.ItemCount, slot.IsEquipped));
        }

        int index = 0;
        foreach(var data in sortedData)
        {
            slots[index].AssignSlotItem(data.Item1, data.Item2, data.Item3);
            index++;
        }
    }


#if UNITY_EDITOR

    public void Test_InventoryPrint()
    {
        // [ 루비 (1/10) , 사파이어 (3,3) ... ]

        string printText = "[";

        for (int i = 0; i < SlotCount - 1; i++)
        {
            if (slots[i].IsEmpty)
            {
                printText += "(빈칸)";
            }
            else
            {
                printText += $"{slots[i].ItemData.itemName}({slots[i].ItemCount}/{slots[i].ItemData.maxStackCount})";
            }
            printText += ", ";
        }

        InvenSlot last = slots[SlotCount - 1];
        if(last.IsEmpty)
        {
            printText += "빈칸";
        }
        else
        {
            printText += $"{last.ItemData.itemName}({last.ItemCount}/{last.ItemData.maxStackCount})";
        }
        printText += "]";

        Debug.Log(printText);
    }

#endif
}
