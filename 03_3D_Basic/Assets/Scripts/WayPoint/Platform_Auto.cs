using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Auto : Platform_OneWay
{

    

    

    protected override void RiderOn(IPlatformRidable target)
    {
        base.RiderOn(target);
        isPause = false;
    }

}
