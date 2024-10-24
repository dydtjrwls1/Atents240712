public interface IStateMachine
{
    // 상태머신의 현재 상태
    IState State { get; }

    // 다른 상태로 전환하는 함수
    void TransitionTo(IState target);
}