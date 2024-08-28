using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformOneWay : PlatformBase
{
    bool playerOn = false;

    float orgSpeed;

    protected override void Awake()
    {
        orgSpeed = moveSpeed;
        moveSpeed = 0;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        playerOn = true;
        moveSpeed = orgSpeed;
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        playerOn = false;
    }

    protected override void OnArrived()
    {
        if (playerOn)
        {
            base.OnArrived();
        }

        moveSpeed = 0.0f;
    }
}
