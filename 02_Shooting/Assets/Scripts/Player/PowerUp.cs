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

    // 캐릭터로 부터 멀어질 확률
    public float fleeChance = 0.7f;

    // 플레이어 트랜스폼
    Transform playerTransform;

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

            StopAllCoroutines(); // 이전 코루틴 제거용(수명은 의미 없어짐)

            // 방향 전환할 회수가 남아있고, 활성화 되어 있을 때
            if (directionChangeCount > 0 && gameObject.activeSelf)
            {
                StartCoroutine(DirectionChange());
            }
            
        }
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

        Vector3 directionFromPlayer = (transform.position - playerTransform.position).normalized;
        Quaternion angle = Quaternion.Euler(Random.Range(-90.0f, 90.0f) * Vector3.forward);
        direction = Random.value > fleeChance ? angle * directionFromPlayer : angle * -directionFromPlayer;

        DirectionChangeCount--;
    }
}
