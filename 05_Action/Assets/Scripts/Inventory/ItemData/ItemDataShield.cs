using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data - Shield", menuName = "Scriptable Objects/Item Data - Shield", order = 7)]
public class ItemDataShield : ItemDataEquip
{
    [Header("방패 데이터")]
    public float defencePower = 15.0f;

    public override EquipType EquipType => EquipType.Shield;
}
