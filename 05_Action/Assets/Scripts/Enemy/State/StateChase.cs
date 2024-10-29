using UnityEngine;

public class StateChase : IState
{
    EnemyStateMachine stateMachine;

    readonly int Move_Hash = Animator.StringToHash("Move");

    public StateChase(EnemyStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;
    }

    public void Enter()
    {
        Debug.Log("상태 진입 - Chase");
        stateMachine.Agent.isStopped = false;
        stateMachine.Animator.SetTrigger(Move_Hash);
    }

    public void Exit()
    {
        Debug.Log("상태 나감 - Chase");
    }

    public void Update()
    {
        if (stateMachine.IsInAttackRange())
        {
            // 플레이어가 공격범위 안에 있으면 공격으로 전환
            stateMachine.TransitionToAttack();
        }
        else if (stateMachine.SearchPlayer(out Vector3 target))
        {
            // 플레이어 발견 시 계속 추적
            stateMachine.Agent.SetDestination(target);
        }
        else
        {
            // 미발견시 잠시 대기
            stateMachine.TransitionToWait();
        }
    }
}