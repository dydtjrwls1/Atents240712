using System;
using UnityEngine;

public interface IBattle
{
    // 이 오브젝트의 트랜스폼
    Transform transform { get; }

    float AttackPower { get; }
    float DefencePower { get; }

    // 피격 이벤트 발생 델리게이트 (int : 실제로 입은 데미지에서 소수점을 제거한 값)
    event Action<int> onHit;

    /// <summary>
    /// 공격 함수
    /// </summary>
    /// <param name="target">공격하는 대상</param>
    void Attack(IBattle target);

    /// <summary>
    /// 방어 함수
    /// </summary>
    /// <param name="damage">적이 나에게 준 데미지</param>
    void Defence(float damage);
}