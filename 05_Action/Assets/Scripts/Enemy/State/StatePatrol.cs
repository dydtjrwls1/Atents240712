using UnityEngine;

public class StatePatrol : IState
{
    EnemyStateMachine stateMachine;

    readonly int Move_Hash = Animator.StringToHash("Move");

    public StatePatrol(EnemyStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;
        
    }

    public void Enter()
    {
        Debug.Log("상태 진입 - Patrol");
        stateMachine.Agent.isStopped = false;
        stateMachine.Agent.SetDestination(stateMachine.Waypoints.NextTarget);
        stateMachine.Animator.SetTrigger(Move_Hash);
    }

    public void Exit()
    {
        Debug.Log("상태 나감 - Patrol");
    }

    public void Update()
    {
        if (stateMachine.SearchPlayer(out Vector3 _))
        {
            // 플레이어 발견 시 추적상태로 전이
            stateMachine.TransitionToChase();
        }
        else
        {
            // 미발견 시 대기
            if (stateMachine.Agent.remainingDistance < 0.25f)
            { 
                stateMachine.Waypoints.StepNextWaypoint();
                stateMachine.Agent.SetDestination(stateMachine.Waypoints.NextTarget);
            }
        }
    }
}