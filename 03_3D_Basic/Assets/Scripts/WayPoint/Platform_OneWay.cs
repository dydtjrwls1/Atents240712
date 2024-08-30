using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_OneWay : PlatformBase
{
    protected bool isPause = true;

    protected override void Start()
    {
        base.Start();
        Target = targetWaypoints.GetNextWayPoint(); // 시작했을 때 Point2로 이동하게끔 지정
    }

    protected override void OnArrived()
    {
        base.OnArrived();
        Debug.Log("도착");
        isPause = true;
    }

    protected override void OnMove(Vector3 moveDelta)
    {
        if (!isPause)
        {
            base.OnMove(moveDelta);
        }
    }
}
