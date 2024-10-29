using UnityEngine;

public class StateWait : IState
{
    EnemyStateMachine stateMachine;

    float waitCountDown;

    readonly int Stop_Hash = Animator.StringToHash("Stop");

    public StateWait(EnemyStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;
    }

    public void Enter()
    {
        waitCountDown = stateMachine.WaitTime;
        stateMachine.Animator.SetTrigger("Stop");
        stateMachine.Agent.isStopped = true;
        stateMachine.Agent.velocity = Vector3.zero;

    }

    public void Exit()
    {
    }

    public void Update()
    {
        if(stateMachine.SearchPlayer(out Vector3 _))
        {
            // 플레이어 발견 시 추적상태로 전이
            stateMachine.TransitionToChase();
        }
        else
        {
            // 미발견 시 대기
            waitCountDown -= Time.deltaTime;
            if (waitCountDown < 0)
            {
                stateMachine.TransitionToPatrol();
            }
        }
    }
}
