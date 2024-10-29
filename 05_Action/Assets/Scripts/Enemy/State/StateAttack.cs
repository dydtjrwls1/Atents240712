using UnityEngine;

public class StateAttack : IState
{
    private EnemyStateMachine stateMachine;

    float attackInterval = 0.0f;
    float attackCoolDown = 0.0f;

    IBattle attackTarget = null;

    public StateAttack(EnemyStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;
        attackInterval = enemyStateMachine.AttackInterval;
    }
    public void Enter()
    {
        Debug.Log("상태 진입 - Attack");
    }

    public void Exit()
    {
        Debug.Log("상태 나감 - Attack");
    }

    public void Update()
    {
        if (stateMachine.IsInAttackRange())
        {
            attackCoolDown -= Time.deltaTime;
            if (attackCoolDown <= 0.0f)
            {
                stateMachine.Attack(attackTarget);
                attackCoolDown = attackInterval;
            }
        }
        else
        {
            stateMachine.TransitionToChase();
        }
    }

    public void SetTarget(IBattle target)
    {
        attackTarget = target;
    }
}