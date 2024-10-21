using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.Burst;
using UnityEngine;

public class PlayerStatus : MonoBehaviour, IHealth, IMana
{
    // HP 와 MP 가 있다.
    // 먹으면 HP 와 MP가 점진적으로 증가하는 아이템 만들기 (Iconsumable 상속) - ItemData_Food, ItemData_Drink
    // Food 는 틱단위로 회복
    // Drink 는 즉시회복
    // 인스팩터 창에서 아이콘 표시하기

    float hp = 100.0f;
    float maxHP = 100.0f;

    float mp = 100.0f;
    float maxMP = 100.0f;

    public float HP
    {
        get => hp;
        private set
        {
            if(IsAlive)
            {
                hp = value;
                if(hp <= 0.0f)
                {
                    Die();
                }

                hp = Mathf.Clamp(hp, 0.0f, maxHP);
                onHealthChange?.Invoke(hp / maxHP);
                Debug.Log($"Current HP : {hp}");
            }
        }
    }

    public float MP
    {
        get => mp;
        set
        {
            if(IsAlive)
            {
                mp = Mathf.Clamp(value, 0f, maxMP);
                onManaChange?.Invoke(mp / maxMP);
                Debug.Log($"Current MP : {mp}");
            }
        }
    }

    public bool IsAlive => hp > 0;

    public float MaxMP => maxMP;

    public float MaxHP => maxHP;

    public event Action<float> onHealthChange = null;
    public event Action onDie;
    public event Action<float> onManaChange;

    public void HealthHeal(float heal)
    {
        HP += heal;
    }

    public void HealthRegenerate(float totalRegen, float duration)
    {
        StartCoroutine(RegenCoroutine(totalRegen, duration, true));
    }

    IEnumerator RegenCoroutine(float totalRegen, float durtaion, bool isHP)
    {
        float regenPerSec = totalRegen / durtaion;
        float elapsedTime = 0.0f;

        while (elapsedTime < durtaion)
        {
            elapsedTime += Time.deltaTime;
            if (isHP)
            {
                HP += Time.deltaTime * regenPerSec;
            }
            else
            {
                MP += Time.deltaTime * regenPerSec;
            }
            yield return null;
        }
    }

    public void HealthRegenerateByTick(float tickRegen, float interval, uint totalTickCount)
    {
        StartCoroutine(RegenByTick(tickRegen, interval, totalTickCount, true));
    }

    IEnumerator RegenByTick(float tickRegen, float interval, uint totalTickCount, bool isHp)
    {
        WaitForSeconds wait = new WaitForSeconds(interval);
        for (int i = 0; i < totalTickCount; i++)
        {
            if (isHp)
            {
                HP += tickRegen;
            }
            else
            {
                MP += tickRegen;
            }
            yield return wait;
        }
    }

    public void Die()
    {
        Debug.Log("사망");
    }

    public void ManaRegenerate(float totalRegen, float duration)
    {
        StartCoroutine(RegenCoroutine(totalRegen, duration, false));
    }

    public void ManaRegenerateByTick(float tickRegen, float interval, uint totalTickCount)
    {
        StartCoroutine(RegenByTick(tickRegen, interval, totalTickCount, false));
    }

    public void ManaHeal(float heal)
    {
        MP += heal;
    }
}
