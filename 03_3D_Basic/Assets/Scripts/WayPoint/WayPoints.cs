using UnityEngine;

public class WayPoints : MonoBehaviour
{
    // 모든 웨이포인트 지점
    Transform[] points;

    // 현재 이동중인 웨이포인트 지점의 인덱스
    int index = 0; 

    // 현재 이동중인 웨이포인트 지점의 트랜스폼
    public Transform CurrentWayPoint => points[index];

    private void Awake()
    {
        // 자식 개수만큼 transform 배열 만들기
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }

    public Transform GetNextWayPoint()
    {
        index++;
        index %= points.Length; // index 의 Out of Range 를 방지하기 위한 코드.

        return points[index];
    }
}