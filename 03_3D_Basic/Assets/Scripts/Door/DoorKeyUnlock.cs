using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeyUnlock : DoorManualStandard, IUnlockable
{
    // true => 열림
    // false => 잠김
    bool unlocked = false;

    public override bool CanUse => base.CanUse && unlocked;

    public void Unlock()
    {
        unlocked = true;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (unlocked) // 잠금이 해제 되었을 때만 단축키 표시
            base.OnTriggerEnter(other);
    }
}
