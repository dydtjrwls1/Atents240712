using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Fire : TrapBase
{
    // 밟으면 fire 파티클 작동, 파티클의 재생시간이 끝나면 정지된다.
    ParticleSystem fireParticle;

    private void Awake()
    {
        fireParticle = GetComponentInChildren<ParticleSystem>(true);
    }

    protected override void OnTrapActivate(GameObject target)
    {
        StartCoroutine(Activate());
    }

    IEnumerator Activate()
    {
        fireParticle.gameObject.SetActive(true);
        fireParticle.Play();
        yield return new WaitForSeconds(fireParticle.main.duration);
        fireParticle.Pause();
        fireParticle.gameObject.SetActive(false);
    }
}
