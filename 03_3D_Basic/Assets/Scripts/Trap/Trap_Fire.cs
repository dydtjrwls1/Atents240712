using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Trap_Fire : TrapBase
{
    // 밟으면 fire 파티클 작동, 파티클의 재생시간이 끝나면 정지된다.
    // 이펙트 재생 시간
    public float duration = 5.0f;

    // 조명 최대 밝기
    public float maxLightIntencity = 2.7f;

    // 조명 최대 범위
    public float maxLightRange = 7.0f;

    ParticleSystem ps;
    Light effectLight;

    private void Awake()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        effectLight = GetComponentInChildren<Light>();
        // effectLight.intensity = 0.0f;                    // 일단 조명 꺼 놓기
        effectLight.range = 0.0f;
    }

    protected override void OnTrapActivate(GameObject target)
    {
        ps.Play();                                              // 이펙트 재생하기
        Player targetPlayer = target.GetComponent<Player>();
        targetPlayer?.Die();

        StopAllCoroutines();                                    // 이전에 재생하던 코루틴 정지
        StartCoroutine(EffectCoroutine());                      // 이펙트 끄기용 코루틴 실행
    }

    IEnumerator EffectCoroutine()
    {
        // yield return new WaitForSeconds(0.1f);                  // 이펙트 재생되고 0.1초 뒤에 조명 켜기

        const float lightTime = 0.2f;
        float remainsTime = lightTime;

        while (remainsTime > 0)
        {
            // effectLight.intensity = Mathf.Lerp(0, maxLightIntencity, (remainsTime - lightTime) * -10);
            effectLight.range = Mathf.Lerp(0, maxLightRange, (remainsTime - lightTime) * -10);
            remainsTime -= Time.deltaTime;

            yield return null;
        }
                      
        yield return new WaitForSeconds(duration - 0.1f);       // 이펙트 재생되고 durtaion 초 뒤에 파티클 정지, 조명 끄기
        ps.Stop();

        remainsTime = lightTime;
        while (remainsTime > 0)
        {
            // effectLight.intensity = Mathf.Lerp(0, maxLightIntencity, remainsTime * 10);
            effectLight.range = Mathf.Lerp(0, maxLightRange, remainsTime * 10);
            remainsTime -= Time.deltaTime;

            yield return null;
        }

        effectLight.range = 0.0f;
    }
}
