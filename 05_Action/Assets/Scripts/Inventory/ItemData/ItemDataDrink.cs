using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data - Drink", menuName = "Scriptable Objects/Item Data - Drink", order = 3)]
public class ItemDataDrink : ItemData, IConsumable
{
    public void Consume(GameObject target)
    {
        PlayerStatus status = target.GetComponent<PlayerStatus>();
        if (status != null)
        {
            status.MP += 1;
        }
    }
}
