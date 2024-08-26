using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Slow : TrapBase
{
    // 슬로우 지속 시간
    public float slowDuration = 5.0f;

    // 느려지는 비율
    [Range(0.1f, 1f)]
    public float slowRate = 0.5f;

    ParticleSystem ps;
    Light effectLight;

    private void Awake()
    {
        Transform child = transform.GetChild(1);
        ps = child.GetComponent<ParticleSystem>();
        effectLight = GetComponentInChildren<Light>();
        effectLight.enabled = false;
    }

    protected override void OnTrapActivate(GameObject target)
    {
        effectLight.enabled = true;
        ps.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        effectLight.enabled = false;
        ps.Stop();
    }
}
