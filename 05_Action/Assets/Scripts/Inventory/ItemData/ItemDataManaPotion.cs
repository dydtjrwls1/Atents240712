using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data - ManaPotion", menuName = "Scriptable Objects/Item Data - ManaPotion", order = 5)]
public class ItemDataManaPotion : ItemData, IUsable
{
    [Header("마나 포션 데이터")]
    // HP에 비례해 즉시 회복할 비율
    public float healRatio = 0.3f;

    public float tickRegen = 1.0f;
    public uint duration = 2;

    public bool Use(GameObject target)
    {
        bool result = false;

        IMana mana = target.GetComponent<IMana>();
        if (mana != null)
        {
            if(mana.MP < mana.MaxMP)
            {
                mana.ManaHeal(mana.MaxMP * healRatio);
                mana.ManaRegenerate(tickRegen, duration);

                result = true;
            }
            else
            {
                Debug.Log("마나가 가득 차서 더이상 회복 불강하다.");
            }
            
        }

        return result;
    }
}
