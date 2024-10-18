using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data - Food", menuName = "Scriptable Objects/Item Data - Food", order = 2)]
public class ItemDataFood : ItemData, IConsumable
{
    public void Consume(GameObject target)
    {
        PlayerStatus playerStatus = target.GetComponent<PlayerStatus>();
        if (playerStatus != null)
        {
            playerStatus.IncreseHP();
        }
    }
}
