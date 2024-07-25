using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : Scrolling
{
    protected override void Awake()
    {
        base.Awake();

        baseLineX = transform.position.x - slotWidth;   // 기준선 계산(현위치에서 슬롯 크기만큼 왼쪽으로 간 x위치)
    }

    protected override void OnMoveRightEnd(int index)
    {
        // 홀짝으로 플립방향 변경
        //int rand = Random.Range(0, 2);
        //spriteRenderers[index].flipX = (rand % 2) != 0; // 홀수면 true, 짝수면 false

        float rand2 = Random.value; // 0 ~ 1 사이의 float 값
        spriteRenderers[index].flipX = rand2 > 0.5f;
    }
}
