using UnityEngine;

public interface IEquipable
{
    // 아이템을 장착할 위치
    EquipType EquipType { get; }

    // 아이템을 장비하는 함수
    void Equip(GameObject target, InvenSlot slot);

    // 아이템 장비를 해제하는 함수
    void UnEquip(GameObject target, InvenSlot slot);

    // 아이템을 장비 또는 해제하는 함수
    void ToggleEquip(GameObject target, InvenSlot slot);
}