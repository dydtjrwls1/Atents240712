using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPlanet : MonoBehaviour
{
    // 배경에 계속 왼쪽으로 이동(moveSpeed)하면서 랜덤한 간격으로 보이는 행성 만들기, 높이도 랜덤
     
    public float moveSpeed = 5.0f;

    public float minRightEnd = 30.0f;
    public float maxRightEnd = 50.0f;

    public float maxY = -1.8f;
    public float minY = -5.0f;

    float baseLineX;

    private void Start()
    {
        baseLineX = transform.position.x;   // 기준선은 시작할 때의 위치
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * Vector3.left);
        if(transform.position.x < baseLineX)
        {
            transform.position = new Vector3(
                Random.Range(minRightEnd, maxRightEnd),
                Random.Range(minY, maxY));
        }
    }
}
