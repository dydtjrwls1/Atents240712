using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    // HP 와 MP 가 있다.
    // 먹으면 HP 와 MP가 점진적으로 증가하는 아이템 만들기 (Iconsumable 상속) - ItemData_Food, ItemData_Drink
    // Food 는 틱단위로 회복
    // Drink 는 즉시회복
    // 인스팩터 창에서 아이콘 표시하기

    public int maxHP = 10;
    public int maxMP = 5;

    int currentHP;
    int currentMP;

    public int HP
    {
        get => currentHP;
        set
        {
            if(currentHP != value)
            {
                currentHP = Mathf.Clamp(value, 0, maxHP);
            }
        }
    }

    public int MP
    {
        get => currentMP;
        set
        {
            if (currentMP != value)
            {
                currentMP = Mathf.Clamp(value, 0, maxMP);
            }
        }
    }
}
