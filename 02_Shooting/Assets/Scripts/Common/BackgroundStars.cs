using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundStars : Scrolling
{
    protected override void Awake()
    {
        base.Awake();   // 부모인 백그라운드의 Awake 함수 실행

        baseLineX = transform.position.x - slotWidth * 0.5f;   // 기준선 계산(현위치에서 슬롯 크기만큼 왼쪽으로 간 x위치)
    }

    protected override void OnMoveRightEnd(int index)
    {
        int rand = Random.Range(0, 4); // 0 ~ 3 사이의 값을 랜덤으로 구하기 ( 나올 수 있는 경우의 수는 4가지이기 때문 )
        // 00 01 10 11

        // 이렇게 까지 하는 이유는 if 를 써서 구현 할 수도 있지만 if 문을 사용해서 코드를 점프하는것 보다 간단하게 연산하는것이 성능적으로 더 좋기 때문이다.
        spriteRenderers[index].flipX = ((rand & 0b_01) != 0);  // 1 아니면 3이다 (첫번째 비트가 1이면 true)
        spriteRenderers[index].flipY = ((rand & 0b_10) != 0);  // 2 아니면 3이다 (두번째 비트가 1이면 true)

        // c#에서 숫자 앞에 "0b_"를 붙이면 2진수라는 의미
        // c#에서 숫자 앞에 "0x_"를 붙이면 16진수라는 의미
    }
}
