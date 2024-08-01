using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : RecycleObject
{
    public float moveSpeed = 5.0f;

    // 방향이 전환되는 시간 간격
    public float directionChangeInterval = 1.0f;

    // 방향 전환이 가능한 최대 회수
    public int directionChangeMaxCount = 5;

    [Range(0, 1)]
    // 캐릭터로 부터 멀어질 확률
    public float fleeChance = 0.7f;

    // 파워업 아이템을 최고 단계일 때 먹으면 얻는 보너스 점수
    public const int BonusPoint = 1000;

    // 애니메이터 파라미터 접근용 해시
    readonly int Count_Hash = Animator.StringToHash("Count");

    // 플레이어 트랜스폼
    Transform playerTransform;

    // 애니메이터
    Animator animator;

    // 현재 이동 방향
    Vector3 direction;

    // 현재 방향 남은 회수
    int directionChangeCount = 0;

    public int DirectionChangeCount
    {
        get => directionChangeCount;
        set
        {
            directionChangeCount = value;
            animator.SetInteger(Count_Hash, directionChangeCount); // 애니메이터에 적용

            StopAllCoroutines(); // 이전 코루틴 제거용(수명은 의미 없어짐)

            // 방향 전환할 회수가 남아있고, 활성화 되어 있을 때
            if (directionChangeCount > 0 && gameObject.activeSelf)
            {
                StartCoroutine(DirectionChange());
            }
            
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 보더에 부딪혔을 때 방향 전환 && 남아 있는 부딪힐 회수가 0 이상일 때
        if (DirectionChangeCount > 0 && collision.gameObject.CompareTag("Border"))
        {
            direction = Vector2.Reflect(direction, collision.contacts[0].normal);
            DirectionChangeCount--;
        }
    }

    protected override void OnReset()
    {
        playerTransform = GameManager.Instance.Player.transform;
        DirectionChangeCount = directionChangeMaxCount;
    }

    /// <summary>
    /// 일정 시간 후에 방향을 전환하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator DirectionChange()
    {
        yield return new WaitForSeconds(directionChangeInterval);

        // fleeChance 확률로 플레이어 반대방향으로 도망가게 만들기
        Vector2 fleeDir = (transform.position - playerTransform.position).normalized; // 플레이어 위치에서 파워업으로 오는 방향
        Quaternion angle = Quaternion.Euler(Random.Range(-90.0f, 90.0f) * Vector3.forward);

        if (Random.value > fleeChance)
        {
            fleeDir = -fleeDir; // 근접할 경우에는 방향 반대로
        }

        direction = angle * fleeDir; // 앞에서 구해진 방향을 +-90도 범위로 회전해서 최종방향 결정

        DirectionChangeCount--;
    }
}
