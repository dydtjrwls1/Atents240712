using UnityEngine;

// 아이템 중 획득 시 즉시 소비되는 아이템에 추가할 인터페이스
public interface IConsumable
{
    void Consume(GameObject target);
}