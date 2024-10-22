using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data - Weapon", menuName = "Scriptable Objects/Item Data - Weapon", order = 6)]
public class ItemDataWeapon : ItemDataEquip
{
    [Header("무기 데이터")]
    public float attackPower = 30.0f;
}
