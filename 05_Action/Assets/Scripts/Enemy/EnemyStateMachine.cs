using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AI;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class EnemyStateMachine : MonoBehaviour
{
    // 대기 상태용 변수들 =====================================

    // 대기 상태 유지 시간
    [SerializeField]
    float waitTime = 1.0f;

    public float WaitTime => waitTime;

    // =====================================================

    // 플레이어 탐색용 변수들 =================================

    // 원거리 시야범위
    [SerializeField]
    float farSightRange = 10.0f;

    // 원거리 시야각의 절반
    [SerializeField]
    float sightHalfAngle = 60.0f;

    // 근거리 시야 범위
    [SerializeField]
    float nearSightRange = 1.5f;


    // =====================================================

    // 순찰 상태용 변수들 =====================================

    [SerializeField]
    Waypoints waypoints;

    public Waypoints Waypoints => waypoints;

    // =====================================================

    // 현재상태
    IState state;

    Animator animator;

    // 전체 상태들
    StateWait wait;
    StatePatrol patrol;
    StateChase chase;

    NavMeshAgent agent;

    public NavMeshAgent Agent => agent;

    public IState State => state;

    public Animator Animator => animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        wait = new StateWait(this);
        patrol = new StatePatrol(this);
        chase = new StateChase(this);

        state = wait;
    }

    private void Update()
    {
        state.Update();
    }

    // 현재 상태에서 다음으로 이동하는 함수
    void TransitionTo(IState target)
    {
        if(target != null)
        {
            state.Exit();
            state = target;
            state.Enter();
        }
    }

    public void TransitionToPatrol()
    {
        TransitionTo(patrol);
    }

    public void TransitionToChase()
    {
        TransitionTo(chase);
    }

    public void TransitionToWait()
    {
        TransitionTo(wait);
    }

    // 플레이어를 탐색하는 함수
    // position => 발견된 위치
    public bool SearchPlayer(out Vector3 position)
    {
        bool result = false;
        position = Vector3.zero;

        // farSightRange 안에 있는 Player 레이어인 모든 Collider 식별
        Collider[] colliders = Physics.OverlapSphere(transform.position, farSightRange, LayerMask.GetMask("Player"));
        if(colliders.Length > 0) // 찾았다면
        {
            IHealth health = colliders[0].GetComponent<IHealth>();
            if(health != null && health.IsAlive) // 살아있을 때만 처리
            {
                Vector3 playerPosition = colliders[0].transform.position; // 플레이어 위치 찾기
                Vector3 toPlayerDir = playerPosition - transform.position; // 슬라임 => 플레이어로 가는 방향 벡터
                if(toPlayerDir.sqrMagnitude < nearSightRange * nearSightRange)
                {
                    // 근거리 범위안에 플레이어가 있다.
                    position = playerPosition;
                    result = true;
                }
                else
                {
                    // 근거리 범위 밖 ~ 원거리 범위 안 사이에 플레이어가 있다.
                    if(IsInSightAngle(toPlayerDir) && IsSightClear(toPlayerDir)) // 시야각 안에 있는지 확인
                    {
                        position = playerPosition;
                        result = true;
                    }
                }
            }
        }

        return result;
    }

    // 시야각 안에 플레이어가 있는지 확인하는 함수
    // toTargetDir => 슬라임이 플레이어를 바라보는 방향 벡터
    bool IsInSightAngle(Vector3 toTargetDir)
    {
        float angle = Vector3.Angle(transform.forward, toTargetDir);

        return angle < sightHalfAngle ? true : false; // sightHalfAnlge 보다 작아야 시야각 안이다.
    }

    /// <summary>
    /// 대상이 플레이어가 다른 오브젝트에 의해 가려지는지 아닌지 확인하는 함수
    /// </summary>
    /// <param name="toTargetDir">슬라임이 플레이어를 바라보는 방향 벡터</param>
    /// <returns>true 면 플레이어가 가려지지 않았다. false 면 가려졌다.</returns>
    bool IsSightClear(Vector3 toTargetDir)
    {
        bool result = false;

        Ray ray = new Ray(transform.position + transform.up * 0.5f, toTargetDir);

        if(Physics.Raycast(ray, out RaycastHit hitInfo,farSightRange))
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                // ray 를 쏴서 처음 충돌하는것이 Player 이다.
                result = true;
            }
        }

        return result;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        bool isPlayerShow = SearchPlayer(out Vector3 _);
        Handles.color = isPlayerShow ? Color.red : Color.blue;

        Handles.DrawWireDisc(transform.position, transform.up, nearSightRange, 5.0f); // 근거리 시야 범위 표시

        Vector3 forward = transform.forward * farSightRange;
        Handles.DrawDottedLine(transform.position, transform.position + forward, 2.0f); // 원거리 중심선 그리기

        Quaternion q1 = Quaternion.AngleAxis(-sightHalfAngle, transform.up);
        Handles.DrawLine(transform.position, transform.position + q1 * forward, 3.0f); // 부채꼴 왼쪽

        Quaternion q2 = Quaternion.AngleAxis(sightHalfAngle, transform.up);
        Handles.DrawLine(transform.position, transform.position + q2 * forward, 3.0f); // 부채꼴 오른쪽

        Handles.DrawWireArc(transform.position, transform.up, q1 * forward, sightHalfAngle * 2.0f, farSightRange, 3.0f);
    }
#endif
}
