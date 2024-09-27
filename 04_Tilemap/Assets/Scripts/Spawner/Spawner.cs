using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Spawner : MonoBehaviour
{
    // 정해진 범위 내에서 일정 시간 간격으로 한마리씩 스폰된다.
    // 정해진 최대 마리수 까지만 스폰된다.
    // 스폰 가능한 위치에서만 스폰이 일어난다.

    // 스폰 간격
    public float interval = 1.0f;

    // 스폰 영역 (x 오른쪽, y 위쪽, 피봇은 transform 의 position)
    public Vector2 size;

    // 스폰 한계치
    public int capacity = 3;

    float elapsedTime = 0.0f;
    int count = 0;

    // 스폰 영역에서 Plain인 노드의 목록
    List<Node> spawnAreaList;

    // 그리드맵, 타일맵 관리하는 객체
    MapArea mapArea;

    private void Awake()
    {
        mapArea = GetComponentInParent<MapArea>();
    }

    private void Start()
    {
        spawnAreaList = mapArea.CalcSpawnArea(transform.position, size);
    }

    private void Update()
    {
        if(count < capacity)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime > interval)
            {
                Spawn();
                elapsedTime = 0.0f;
            }
        }
    }

    void Spawn()
    {
        if(IsSpawnAvailable(out Vector3 spawnPosition))
        {
            Slime slime = Factory.Instance.GetSlime(spawnPosition);
            slime.Initialized(mapArea.GridMap, spawnPosition);
            slime.transform.parent = transform;
            if(slime.onDie == null)
            {
                slime.onDie += () =>
                {
                    count--;
                };
            }

            count++;
        }
    }

    // 스폰 가능한 지역이 있는지 확인하고 리턴해주는 함수
    bool IsSpawnAvailable(out Vector3 spawnablePosition)
    {
        bool result = false;
        List<Node> positions = new List<Node>(); // 지금 스폰 가능한 지역 의 목록
        
        foreach(var node in spawnAreaList)
        {
            if(node.nodeType == Node.NodeType.Plain) // 지금 평지인 지역 찾기
                positions.Add(node);
        }

        if(positions.Count > 0)
        {
            int index =  Random.Range(0, positions.Count);
            Node target = positions[index];
            spawnablePosition = mapArea.GridToWorld(target.X, target.Y);

            result = true;
        }
        else
        {
            // 스폰 가능한 노드가 없다.
            spawnablePosition = Vector3.zero;
        }

        return result;
    }

    

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector3 p0 = new(Mathf.Floor(transform.position.x), Mathf.Floor(transform.position.y));
        Vector3 p1 = p0 + Vector3.right * size.x;
        Vector3 p2 = p0 + (Vector3)size;
        Vector3 p3 = p0 + Vector3.up * size.y;

        Handles.color = Color.red;
        Handles.DrawLine(p0, p1, 5);
        Handles.DrawLine(p1, p2, 5);
        Handles.DrawLine(p2, p3, 5);
        Handles.DrawLine(p3, p0, 5);
    }
#endif
}
