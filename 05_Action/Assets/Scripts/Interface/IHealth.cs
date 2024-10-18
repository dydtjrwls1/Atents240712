using System;
using UnityEngine;

public interface IHealth
{
    // HP 확인용 프로퍼티
    float HP { get; }

    // 최대 HP 확인용 프로퍼티
    float MaxHP { get; }

    // 생존 확인용 프로퍼티
    bool IsAlive { get; }

    // HP 변화를 알리는 델리게이트 (float : 변화비율)
    event Action<float> onHealthChange;

    // 사망을 알리는 델리게이트
    event Action onDie;

    /// <summary>
    /// HP를 지속적으로 회복시키는 함수
    /// </summary>
    /// <param name="totalRegen">전체 회복량</param>
    /// <param name="duration">회복 기간</param>
    void HealthRegenerate(float totalRegen, float duration);

    /// <summary>
    /// 틱 단위로 HP를 지속적으로 회복시키는 함수
    /// </summary>
    /// <param name="tickRegen">틱 당 회복량</param>
    /// <param name="interval">틱 시간 간격</param>
    /// <param name="totalTickCount">전체 틱 수</param>
    void HealthRegenerateByTick(float tickRegen, float interval, uint totalTickCount);

    /// <summary>
    /// HP를 즉시 회복시키는 함수
    /// </summary>
    /// <param name="heal">회복량</param>
    void HealthHeal(float heal);

    /// <summary>
    /// 사망 처리용 함수
    /// </summary>
    void Die();
}