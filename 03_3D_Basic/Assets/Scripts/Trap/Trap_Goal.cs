using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Goal : TrapBase
{
    public Action onGoal;

    protected override void OnTrapActivate(GameObject target)
    {
        onGoal?.Invoke();
    }
}
