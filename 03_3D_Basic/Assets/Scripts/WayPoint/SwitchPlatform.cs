using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlatform : SwitchBase<Platform_Manual>
{
    public override bool CanUse => target.CanUse;
}
