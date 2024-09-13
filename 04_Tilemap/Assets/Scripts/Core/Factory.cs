using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : SingleTon<Factory>
{
    SlimeObjectPool slime;

    protected override void OnInitialize()
    {
        slime = GetComponentInChildren<SlimeObjectPool>();
        slime?.Initialize();
    }

    public Slime GetSlime(Vector3? position)
    {
        return slime.GetObject(position);
    }
}
