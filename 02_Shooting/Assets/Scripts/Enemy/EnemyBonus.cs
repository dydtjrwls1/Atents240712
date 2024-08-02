using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBonus : EnemyBase
{
    [Header("보너스 적 데이터")]
    // 등장 시간
    public float appearTime = 0.5f;

    // 대기 시간
    public float waitTime = 3.0f;

    // 대기 시간 이후 속도
    public float secondSpeed = 10.0f;

    Animator animator;

    readonly int SpeedHash = Animator.StringToHash("Speed");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void OnReset()
    {
        base.OnReset();

        StartCoroutine(AppearProcess());
    }

    IEnumerator AppearProcess()
    {
        animator.SetFloat(SpeedHash, speed);
        yield return new WaitForSeconds(appearTime);

        speed = 0.0f;
        animator.SetFloat(SpeedHash, speed);

        yield return new WaitForSeconds(waitTime);

        speed = secondSpeed;
        animator.SetFloat(SpeedHash, speed);
    }

    protected override void OnDie()
    {
        Factory.Instance.GetPowerUp(transform.position);
    }
}
