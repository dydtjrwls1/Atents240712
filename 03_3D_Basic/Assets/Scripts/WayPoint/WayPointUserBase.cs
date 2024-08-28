using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointUserBase : MonoBehaviour
{
    // 이 오브젝트가 따라 움직일 경로를 가진 웨이포인트
    public WayPoints targetWaypoints;

    // 이동 속도
    public float moveSpeed = 5.0f;

    // 오브젝트의 이동 방향
    Vector3 moveDirection;

    // 현재 목표로 하고 있는 웨이포인트 지점의 트랜스폼
    Transform target;

    Rigidbody rb;

    // 목표로할 웨이포인트를 지정하고 확인하는 프로퍼티
    protected virtual Transform Target
    {
        get => target;
        set
        {
            target= value;
            moveDirection = (target.position - transform.position).normalized;
        }
    }

    // 현재 목표지점에 근접했는지 확인해주는 프로퍼티(true 도착, false 아직 도착 X)
    bool IsArrived
    {
        get => (target.position - transform.position).sqrMagnitude < 0.0025f; // 도착지점가지의 거리가 0.1보다 작으면 도착했다고 판단
    }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        Target = targetWaypoints.CurrentWayPoint;
    }

    private void FixedUpdate()
    {
        OnMove();
    }

    protected virtual void OnMove()
    {
        if(IsArrived)
        {
            OnArrived();
        }

        // Vector3.MoveTowards(); 정확한 위치로 갈 수 있지만 연산 부담이 크다.
        Vector3 nextPosition = rb.position + (Time.fixedDeltaTime * moveSpeed * moveDirection);
        rb.MovePosition(nextPosition);
    }

    // 웨이포인트에 도착했을 때 실행될 함수
    protected virtual void OnArrived()
    {
        Target = targetWaypoints.GetNextWayPoint();
        
    }
}
