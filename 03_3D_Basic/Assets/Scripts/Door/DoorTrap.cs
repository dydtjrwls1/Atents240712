using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrap : DoorManualStandard
{
    ParticleSystem ps;

    protected override void Awake()
    {
        base.Awake();
        Transform child = transform.GetChild(3);
        ps = child.GetComponent<ParticleSystem>();
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        ps.Play();
        // 코루틴의 파라메터 = 메인 모듈의 재생시간 + 파티클 입자 하나의 최대 수명
        StartCoroutine(AutoClose(ps.main.duration + ps.main.startLifetime.constantMax)); 
        GameManager.Instance.Player.Die();
    }

    IEnumerator AutoClose(float delay)
    {
        yield return new WaitForSeconds(delay);
        Close();
    }
}
