using UnityEngine;

public class StateDie : IState
{
    private EnemyStateMachine stateMachine;

    public StateDie(EnemyStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;
    }
    public void Enter()
    {
        Debug.Log("상태 진입 - Die");
    }

    public void Exit()
    {
        Debug.Log("상태 나감 - Die");
    }

    public void Update()
    {
    }
}