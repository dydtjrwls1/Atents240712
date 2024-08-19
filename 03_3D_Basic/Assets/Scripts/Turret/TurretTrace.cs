using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TurretTrace : TurretBase
{
    // 플레이어 방향으로 Gun이 회전한다.
    // 사정거리 안으로 플레이어가 들어오면 대가리 회전하고 총알 발사

    // 시야각 적용해보기
    // 시야 가려짐 적용해보기

    // 사정거리
    [Header("Trace 터렛용 데이터")]
    public float sightRange = 10.0f;

    // 회전 속도용 계수
    public float turnSmooth = 2.0f;

    // 시야범위 체크용 트리거
    SphereCollider sightTrigger;

    // 타겟 위치
    Transform target = null;


    protected override void Awake()
    {
        base.Awake();

        sightTrigger = GetComponent<SphereCollider>();
        sightTrigger.radius = sightRange;
    }

    private void Update()
    {
        LookTargetAndAttack();
        // gun.forward = target? Vector3.Lerp(gun.forward, target.position - transform.position, Time.deltaTime * turnSmooth) : gun.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            target = other.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            target = null;
            
    }

    void LookTargetAndAttack()
    {
        if (target)
        {
            Vector3 direction = target.position - transform.position; // 플레이어를 바라보는 방향
            direction.y = 0.0f; // xz평면으로만 회전하게 하기 위해 y는 제거

            gun.rotation = Quaternion.Slerp(
                gun.rotation, Quaternion.LookRotation(direction), Time.deltaTime * turnSmooth);

            StartFire();
        } else
        {
            StopFire();
        }
    }

    // 현재 발사 중인지를 기록하는 변수
    bool isFiring = false;

    void StartFire()
    {
        if (!isFiring)
        {
            isFiring = true;
            StartCoroutine(fire);
        }
    }

    void StopFire()
    {
        if (isFiring)
        {
            StopCoroutine(fire);
            isFiring = false;
        }
    }

#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, transform.up, sightRange, 3.0f);
    }
#endif
}