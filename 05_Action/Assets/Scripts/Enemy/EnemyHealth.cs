using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyStateMachine))]
public class EnemyHealth : MonoBehaviour, IHealth
{
    float hp = 100.0f;

    [SerializeField]
    float maxHP = 100.0f;

    EnemyStateMachine stateMachine;

    public float HP
    {
        get => hp;
        private set
        {
            if (stateMachine.IsAlive)
            {
                hp = value;

                // 살아있고 hp 가 0 이하일경우 사망상태로 전환
                if (stateMachine.IsAlive && hp <= 0.0f)
                {
                    Die();
                }

                hp = Mathf.Clamp(hp, 0.0f, maxHP);
                onHealthChange?.Invoke(hp / maxHP);
            }
        }
    }

    public float MaxHP => maxHP;

    public bool IsAlive => hp > 0.0f;

    public event Action<float> onHealthChange;
    public event Action onDie;

    private void Awake()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
    }

    public void Die()
    {
        Debug.Log("슬라임 사망");
        stateMachine.TransitionToDie();
        onDie?.Invoke();
    }

    public void HealthHeal(float heal)
    {
        HP += heal;
    }

    public void HealthRegenerate(float totalRegen, float duration)
    {
        throw new NotImplementedException();
    }

    public void HealthRegenerateByTick(float tickRegen, float interval, uint totalTickCount)
    {
        throw new NotImplementedException();
    }

    public void GetDamage(float damage)
    {
        HP -= damage;
    }
}
