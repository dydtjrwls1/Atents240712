using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBase : WayPointUserBase
{
    Action<Vector3> platformMove = null;

    protected override void Start()
    {
        if (targetWaypoints == null)
        {
            int nextIndex = transform.GetSiblingIndex() + 1; // 동생의 인덱스 구하기
            Transform nextTransform = transform.parent.GetChild(nextIndex);
            targetWaypoints = nextTransform.GetComponent<WayPoints>();
        }
        base.Start();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {        
        IPlatformRidable target = other.GetComponent<IPlatformRidable>();
        if(target != null)
        {
            RiderOn(target);
            
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        IPlatformRidable target = other.GetComponent<IPlatformRidable>();
        if (target != null)
        {
            RiderOff(target);
        }
    }

    protected override void OnMove(Vector3 moveDelta)
    {
        base.OnMove(moveDelta);

        platformMove?.Invoke(moveDelta);
    }

    /// <summary>
    /// 플랫폼 위에 IPlatformRidable을 가진 타겟이 올라왔을 때 실행
    /// </summary>
    /// <param name="target">올라온 대상</param>
    protected virtual void RiderOn(IPlatformRidable target)
    {
        platformMove += target.OnRidePlatform;
    }

    /// <summary>
    /// 플롯폼에서 IPlatformRidable 을 가진 타겟이 나갔을 때 실행
    /// </summary>
    /// <param name="target">나간 대상</param>
    protected virtual void RiderOff(IPlatformRidable target)
    {
        platformMove -= target.OnRidePlatform;
    }
}
