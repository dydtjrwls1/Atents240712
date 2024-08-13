using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRandomize : MonoBehaviour
{
    // 클릭 할 때마다 리롤하는 것이 목적인 변수
    public bool reroll = true;

    [Range(0, 0.5f)]
    // 랜덤 정도
    public float randomizeRange = 0.15f;

    // inspector 창에서 값이 성공적으로 변경되었을 때 실행되는 이벤트 함수
    private void OnValidate()
    {
        if (reroll)
        {
            Randomize();
            reroll = false;
        }
    }

    public void Randomize()
    {
        transform.localScale = new Vector3(
            1 + Random.Range(-randomizeRange, randomizeRange), 
            1 + Random.Range(-randomizeRange, randomizeRange),
            1 + Random.Range(-randomizeRange, randomizeRange));

        transform.Rotate(0, Random.Range(0, 360), 0); // y 축 랜덤 회전
    }
}
