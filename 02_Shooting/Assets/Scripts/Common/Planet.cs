using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    // 행성의 X 좌표가 경계선보다 왼쪽일 경우 행성의 위치를 초기화 한다. 경계선의 위치는 랜덤하게 결정한다.

    // X 경계선의 범위
    float boundRange_left = -30.0f;
    float boundRange_right = -16.0f;

    // X 경계선의 위치를 담을 변수
    float boundX = -20.0f;

    // 행성이 초기화될 위치의 X 값
    float defaultX;

    // 행성의 Y 값 범위
    public float upperRangeY = -2.0f;
    public float lowerRangeY = -3.0f;

    // 현재 행성이 위치할 Y 좌표
    public float currentY = -2.0f;

    // 스크롤 속도
    public float scrollSpeed = 3.0f;

    private void Awake()
    {
        defaultX = transform.position.x;
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * scrollSpeed * Vector2.left, Space.World);
        if (transform.position.x < boundX)
        {
            boundX = Random.Range(boundRange_left, boundRange_right);
            currentY = Random.Range(lowerRangeY, upperRangeY);

            transform.position = new Vector3(defaultX, currentY);
        }
    }
}
