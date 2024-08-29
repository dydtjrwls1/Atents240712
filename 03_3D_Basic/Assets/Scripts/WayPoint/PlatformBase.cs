using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBase : WayPointUserBase
{
    Player player;

    public Action<Vector3> onPlatformMove = null;

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


    protected override void OnMove(Vector3 moveDelta)
    {
        base.OnMove(moveDelta);
        onPlatformMove?.Invoke(moveDelta);
    }
}
