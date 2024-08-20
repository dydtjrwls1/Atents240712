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

    // 터렛이 총알 발사를 시작하는 좌우 발사각 (10일 경우 +-10)
    public float fireAngle = 10.0f;

    // 시야범위 체크용 트리거
    SphereCollider sightTrigger;

    // 타겟 위치
    Transform target = null;

    // 발사 재시작용 쿨타임
    float fireCoolTime = 0.0f;

#if UNITY_EDITOR
    // 발사할 수 있는 상황인지 확인하는 변수
    bool isFireReady;
#endif

    protected override void Awake()
    {
        base.Awake();

        sightTrigger = GetComponent<SphereCollider>();
        sightTrigger.radius = sightRange;
    }

    private void Update()
    {
        fireCoolTime -= Time.deltaTime;
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

    /// <summary>
    /// 플레이어 추적 및 발사 처리용 함수
    /// </summary>
    void LookTargetAndAttack()
    {
        bool isStartFire = false;

        if (target)
        {
            Vector3 direction = target.position - transform.position; // 플레이어를 바라보는 방향
            direction.y = 0.0f; // xz평면으로만 회전하게 하기 위해 y는 제거

            if (IsTargetVisible(direction)) // 타겟이 보이고 있다.
            {
                gun.rotation = Quaternion.Slerp(
                gun.rotation, Quaternion.LookRotation(direction), Time.deltaTime * turnSmooth); //target 을 추적한다.

                float targetAngle = Vector3.Angle(gun.forward, direction);
                if (targetAngle < fireAngle)
                {
                    // 사이각이 fireAngle 보다 작다. 
                    isStartFire = true;
                }
            }
        }

#if UNITY_EDITOR
        isFireReady = isStartFire;
#endif

        if (isStartFire && fireCoolTime < 0.0f) // 발사 중이 아닐 때 && 쿨타임이 0초 미만일 때
        {
            StartFire();
        }
        else
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
            fireCoolTime = fireInterval; // 쿨타임 초기화
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

    /// <summary>
    /// 추적 대상이 보이는지 확인하는 함수
    /// </summary>
    /// <param name="lookDirection">바라보는 방향</param>
    /// <returns>true 면 보인다, false 면 안보인다.</returns>
    bool IsTargetVisible(Vector3 lookDirection)
    {
        bool result = false;

        Ray ray = new Ray(gun.position, lookDirection);

        // out : 출력용 파라메터라고 알려주는 키워드. 함수가 실행되면 자동으로 초기화된다.
        int mask = int.MaxValue;
        int bulletMask = LayerMask.GetMask("Bullet");
        bulletMask = ~bulletMask;
        mask = mask & bulletMask;  // mask는 총알을 제외한 모든 레이어가 세팅되어 있음
        
        if (Physics.Raycast(ray, out RaycastHit hitInfo, sightRange, mask))
        {
            // ray에 닿은 오브젝트가 있다.
            if(hitInfo.transform == target.transform) // 첫 번째로 닿은 오브젝트가 target 이다 (= 가리는 물체가 없다)
            {
                result = true;
            }
        }

        return result;
    }

#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Handles.color = Color.white;
        Handles.DrawWireDisc(transform.position, transform.up, sightRange, 3.0f);

        Handles.color = Color.yellow;

        if (gun == null)
            gun = transform.GetChild(2);
        Vector3 from = transform.position;
        Vector3 to = transform.position + gun.forward * sightRange;
        Handles.DrawDottedLine(from, to, 2.0f);

        // 발사각 그리기

        // 녹색 : 내 시야 범위안에 플레이어가 없는 상태일 때
        // 주황색 : 내 시야범위안에 플레이어가 있고 발사를 할 수 없는 상태일 때 (시야각 밖이거나 가려지는 물체가 있다.)
        // 빨간색 : 내 시야범위안에 플레이어가 있고 발사를 할 수 있는 상태일 때 (시야각 안이고 가려지는 물체도 없다.)

        // Handles.DrawWireArc()
        if (target == null)
        {
            Handles.color = Color.green;
        }
        else
        {
            if (isFireReady)
            {
                Handles.color = Color.red;
            }
            else
            {
                Handles.color = new Color(1, 0.5f, 0);
            }
        }
        


        Vector3 startDir = Quaternion.AngleAxis(-fireAngle, transform.up) * gun.forward;
        Vector3 endDir = Quaternion.AngleAxis(fireAngle, transform.up) * gun.forward;

        from = transform.position + startDir * sightRange;
        to = transform.position + endDir * sightRange;

        Handles.DrawLine(transform.position, from, 3.0f);
        Handles.DrawLine(transform.position, to, 3.0f);
        Handles.DrawWireArc(transform.position, transform.up, startDir, fireAngle * 2, sightRange, 3.0f);
    }
#endif
}