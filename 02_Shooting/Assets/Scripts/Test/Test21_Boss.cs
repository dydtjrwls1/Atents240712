using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test21_Boss : TestBase
{
    Transform target;

    private void Awake()
    {
        target = transform.GetChild(0);
    }
}

