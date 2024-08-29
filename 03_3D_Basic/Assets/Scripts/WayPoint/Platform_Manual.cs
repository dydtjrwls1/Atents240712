using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Manual : PlatformBase, IInteractable
{
    bool usable = true;

    float orgSpeed;

    public bool CanUse 
    {
        get => usable;
        set
        {
            usable = value;
            moveSpeed = usable ? 0.0f : orgSpeed;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        orgSpeed = moveSpeed;
    }

    public void Use()
    {
        CanUse = false;
    }

    protected override void OnArrived()
    {
        base.OnArrived();
        CanUse = true;
    }
}
