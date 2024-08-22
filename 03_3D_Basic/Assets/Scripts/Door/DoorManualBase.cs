using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorManualBase : DoorBase, IInteractable
{
    protected bool isOpen = false;

    public virtual void Use()
    {
        if (isOpen)
            Close();
        else
            Open();
    }

    public virtual bool CanUse => true;

    /// <summary>
    /// 문이 열렸음을 표시하는 기능
    /// </summary>
    protected override void OnOpen()
    {
        isOpen = true;
    }

    /// <summary>
    /// 문이 닫혔음을 표시하는 기능
    /// </summary>
    protected override void OnClose()
    {
        isOpen = false;
    }
}
