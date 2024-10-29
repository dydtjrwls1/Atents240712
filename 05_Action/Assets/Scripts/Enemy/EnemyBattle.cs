using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(EnemyHealth))]
public class EnemyBattle : MonoBehaviour, IBattle
{
    [SerializeField]
    float attackPower = 10.0f;

    [SerializeField]
    float defencePower = 3.0f;

    [SerializeField]
    float attackInterval = 1.0f;

    float attackCoolDown = 0.0f;

    Animator animator;

    EnemyHealth health;

    readonly int Attack_Hash = Animator.StringToHash("Attack");
    readonly int Hit_Hash = Animator.StringToHash("Hit");

    public float AttackPower => attackPower;

    public float DefencePower => defencePower;

    public float AttackInterval => attackInterval;

    public event Action<int> onHit;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<EnemyHealth>();
    }

    public void Attack(IBattle target)
    {
        target.Defence(AttackPower);
        animator.SetTrigger(Attack_Hash);
    }

    public void Defence(float damage)
    {
        if (health.IsAlive)
        {
            animator.SetTrigger(Hit_Hash);

            float final = Mathf.Max(1f, damage - defencePower);
            health.GetDamage(final);
            onHit?.Invoke(Mathf.RoundToInt(final));
        }
    }

    public void TryAttackAndLook(IBattle attackTarget)
    {
        attackCoolDown -= Time.deltaTime;

        if (attackCoolDown <= 0.0f)
        {
            Attack(attackTarget);
            ResetAttackCoolTime();
        }

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(attackTarget.transform.position - transform.position),
            0.1f);
    }

    public void ResetAttackCoolTime()
    {
        attackCoolDown = attackInterval;
    }
}
