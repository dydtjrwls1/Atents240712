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

    Player target = null;

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

        this.target = target.GetComponent<Player>();
        this.target.SetSlowDebuff(slowRate);
    }

    private void OnTriggerExit(Collider other)
    {
        if (target.CompareTag("Player"))
        {
            Debug.Log("벗어남");
            effectLight.enabled = false;
            ps.Stop();

            target?.RemoveSlowDebuff(slowDuration);
        }
    }
}
