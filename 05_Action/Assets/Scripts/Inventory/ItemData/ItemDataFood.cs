using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 음식 아이템용 ItemData
[CreateAssetMenu(fileName = "New Item Data - Food", menuName = "Scriptable Objects/Item Data - Food", order = 2)]
public class ItemDataFood : ItemData, IConsumable
{
    [Header("음식 아이템 데이터")]
    public float tickRegen = 1.0f;
    public float interval = 1.0f;
    public uint totalTickCount = 1;

    public void Consume(GameObject target)
    {
        IHealth health = target.GetComponent<IHealth>();
        if(health != null)
        {
            health.HealthRegenerateByTick(tickRegen, interval, totalTickCount);
        }
    }
}
