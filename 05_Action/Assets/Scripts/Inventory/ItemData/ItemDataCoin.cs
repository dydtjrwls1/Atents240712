using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 동전 아이템용 ItemData
[CreateAssetMenu(fileName = "New Item Data - Coin", menuName = "Scriptable Objects/Item Data - Coin", order = 1)]
public class ItemDataCoin : ItemData, IConsumable
{
    public void Consume(GameObject target)
    {
        IMoneyContainer moneyContainer = target.GetComponent<IMoneyContainer>();
        if (moneyContainer != null )    // target이 돈을 담을 수 있으면 돈을 증가시킨다.
        {
            moneyContainer.Money += (int)price;
        }
    }
}
