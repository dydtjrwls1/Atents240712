using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data - Drink", menuName = "Scriptable Objects/Item Data - Drink", order = 3)]
public class ItemDataDrink : ItemData, IConsumable
{
    [Header("음료 아이템 데이터")]
    public float tickRegen = 1.0f;
    public uint duration = 2;

    public void Consume(GameObject target)
    {
        IMana mana = target.GetComponent<IMana>();
        if (mana != null)
        {
            mana.ManaRegenerate(tickRegen, duration);
        }
    }
}
