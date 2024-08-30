using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Manual : Platform_OneWay, IInteractable
{
    public bool CanUse => throw new System.NotImplementedException();

    public void Use()
    {
        isPause = false;
    }

    
}
