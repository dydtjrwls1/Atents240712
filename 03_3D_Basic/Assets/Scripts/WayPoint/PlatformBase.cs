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
            platformMove += target.OnRidePlatform;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        IPlatformRidable target = other.GetComponent<IPlatformRidable>();
        if (target != null)
        {
            platformMove -= target.OnRidePlatform;
        }
    }

    protected override void OnMove(Vector3 moveDelta)
    {
        base.OnMove(moveDelta);

        platformMove?.Invoke(moveDelta);
    }
}
