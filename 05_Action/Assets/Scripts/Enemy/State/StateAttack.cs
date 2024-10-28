using UnityEngine;

public class StateAttack : IState
{
    private EnemyStateMachine stateMachine;

    public StateAttack(EnemyStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;
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
        throw new System.NotImplementedException();
    }
}