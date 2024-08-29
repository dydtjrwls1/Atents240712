using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Auto : PlatformBase
{
    float orgSpeed;

    protected override void Awake()
    {
        orgSpeed = moveSpeed;
        moveSpeed = 0;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        
        moveSpeed = orgSpeed;
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }

    protected override void OnArrived()
    {
        base.OnArrived();
        moveSpeed = 0.0f;
        
    }
}
