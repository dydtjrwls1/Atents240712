using UnityEngine;

public interface IEquipTarget
{
    // 특정 부위에 어느 슬롯에 있는 아이템이 장비되었는지 
    // 또는 장비되지 않았는지 확인하기 위한 인덱서 (null 이면 장비되지 않음)
    InvenSlot this[EquipType equipType] { get; }

    void EquipItem(EquipType part, InvenSlot slot);

    void UnEquipItem(EquipType equipType);

    // 아이템이 장착된 트랜스폼을 반환하는 트랜스폼
    Transform GetEquipParentTransform(EquipType part);
}