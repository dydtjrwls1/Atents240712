using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataEquip : ItemData, IEquipable
{
    [Header("장비 아이템 데이터")]
    // 아이템을 장비 했을 때 생성하는 프리펩
    public GameObject equipPrefab;

    public virtual EquipType EquipType => EquipType.Weapon;

    public void Equip(GameObject target, InvenSlot slot)
    {
        IEquipTarget equipTarget = target.GetComponent<IEquipTarget>();
        if (equipTarget != null )
        {
            equipTarget.EquipItem(EquipType, slot);
        }
    }

    public void ToggleEquip(GameObject target, InvenSlot slot)
    {
        IEquipTarget equipTarget = target.GetComponent<IEquipTarget>();
        if (equipTarget != null)
        {
            InvenSlot oldSlot = equipTarget[EquipType];
            if(oldSlot != null) 
            {
                // 무언가 장비되어있다
                UnEquip(target, oldSlot);
                if(oldSlot != slot)
                {
                    // 장비되어 있는것의 종류가 다르다
                    Equip(target, slot);
                }
            }
            else
            {
                Equip(target, slot);
            }
        }
    }

    public void UnEquip(GameObject target, InvenSlot slot)
    {
        IEquipTarget equipTarget = target.GetComponent<IEquipTarget>();
        if (equipTarget != null)
        {
            equipTarget.UnEquipItem(EquipType);
        }
    }
}
