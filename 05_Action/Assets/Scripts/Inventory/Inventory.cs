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
}
