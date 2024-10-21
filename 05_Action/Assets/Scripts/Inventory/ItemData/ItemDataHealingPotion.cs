using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 최대 HP 의 비례한 즉시회복 + 도트 힐
[CreateAssetMenu(fileName = "New Item Data - HealingPotion", menuName = "Scriptable Objects/Item Data - HealingPotion", order = 4)]
public class ItemDataHealingPotion : ItemData, IUsable
{
    [Header("힐링 포션 데이터")]
    // HP에 비례해 즉시 회복할 비율
    public float healRatio = 0.3f;

    // 틱 당 회복량
    public float tickRegen = 5.0f;

    // 틱 간격
    public float tickInterval = 1.0f;

    // 틱 횟수
    public uint totalTickCount = 5;

    public bool Use(GameObject target)
    {
        bool result = false;

        IHealth health = target.GetComponent<IHealth>();
        if (health != null)
        {
            // HP가 최대 HP보다 적을 때만 실행
            if(health.HP < health.MaxHP)
            {
                // 즉시 회복후 틱당 회복
                health.HealthHeal(health.MaxHP * healRatio);
                health.HealthRegenerateByTick(tickRegen, tickInterval, totalTickCount);

                result = true;
            }
            else
            {
                Debug.Log("HP가 가득 차서 더이상 회복이 불가합니다.");
            }
        }

        return result;
    }

}
