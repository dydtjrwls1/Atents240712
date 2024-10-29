using UnityEngine;

public class StateAttack : IState
{
    private EnemyStateMachine stateMachine;

    EnemyBattle battle;

    public StateAttack(EnemyStateMachine enemyStateMachine, EnemyBattle enemyBattle)
    {
        stateMachine = enemyStateMachine;
        battle = enemyBattle;
    }
    public void Enter()
    {
        battle.ResetAttackCoolTime();
        stateMachine.Agent.isStopped = true;
        stateMachine.Agent.velocity = Vector3.zero;
    }

    public void Exit()
    {
        Debug.Log("상태 나감 - Attack");
    }

    public void Update()
    {
        IBattle attackTarget = stateMachine.PlayerInAttackRange(); // 공격 범위 안에있는 플레이어 받아오기

        if (attackTarget != null)
        {
            battle.TryAttackAndLook(attackTarget); // 공격 대상이 있으면 공격 시도
        }
        else
        {
            stateMachine.TransitionToChase(); // 공격 대상이 없으면 추적 상태로 전환
        }
    }
}