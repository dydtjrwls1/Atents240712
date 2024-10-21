using System;

public interface IMana
{
    float MP { get; }

    float MaxMP { get; }

    // HP 변화를 알리는 델리게이트 (float : 변화비율)
    event Action<float> onManaChange;

    /// <summary>
    /// MP를 지속적으로 회복시키는 함수
    /// </summary>
    /// <param name="totalRegen">전체 회복량</param>
    /// <param name="duration">회복 기간</param>
    void ManaRegenerate(float totalRegen, float duration);

    /// <summary>
    /// 틱 단위로 MP를 지속적으로 회복시키는 함수
    /// </summary>
    /// <param name="tickRegen">틱 당 회복량</param>
    /// <param name="interval">틱 시간 간격</param>
    /// <param name="totalTickCount">전체 틱 수</param>
    void ManaRegenerateByTick(float tickRegen, float interval, uint totalTickCount);

    /// <summary>
    /// MP를 즉시 회복시키는 함수
    /// </summary>
    /// <param name="heal">회복량</param>
    void ManaHeal(float heal);
}