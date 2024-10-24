public interface IState
{
    // 이 상태에 진입했을 때 실행되는 함수
    void Enter();

    // 이 상태에서 나갈 때 실행되는 함수
    void Exit();

    // 이 상태가 매 프레임마다 해야 할 일을 기록해 놓은 함수
    void Update();
}
