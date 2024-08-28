using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBase : WayPointUserBase
{
    Player player;

    public Action<Vector3> platformMove = null;

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
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();
            platformMove += (delta) => player.MoveWithObject(delta);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
            platformMove = null;
        }
    }

    protected override void OnMove()
    {
        base.OnMove();

        Vector3 nextPosition = Time.fixedDeltaTime * moveSpeed * moveDirection;
        platformMove?.Invoke(nextPosition);
    }
}
