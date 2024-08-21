using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManualAutoClosing : DoorManual
{
    public float closeTime = 3.0f;

    protected override void OnOpen()
    {
        StopAllCoroutines();
        StartCoroutine(AutoClose(closeTime));
    }

    IEnumerator AutoClose(float time)
    {
        yield return new WaitForSeconds(time);
        Close();
    }
}
