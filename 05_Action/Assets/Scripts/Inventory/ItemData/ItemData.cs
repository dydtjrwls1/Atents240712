using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Objects/Item Data", order = 0)]
public class ItemData : ScriptableObject
{
    [Header("아이템 기본 정보")]
    public ItemCode code = ItemCode.Misc;
    public string itemName = "아이템";
    public string itemDesc = "아이템 설명";
    public Sprite itemIcon;
    public uint price = 0;

    [Min(1)]
    public uint maxStackCount = 1;


}
