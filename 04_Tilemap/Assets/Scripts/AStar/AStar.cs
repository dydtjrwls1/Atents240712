using System;
using System.Collections.Generic;
using UnityEngine;

public static class AStar
{
    // 옆으로 이동하는 거리
    //const float sideDistance = 1.0f;
    const float sideDistance = 10f;

    // 대각선으로 이동하는 거리
    //const float diagonalDistance = 1.4142135f;
    const float diagonalDistance = 14f;

    /// <summary>
    /// 경로를 찾는 함수
    /// </summary>
    /// <param name="map">경로를 찾을 맵</param>
    /// <param name="start">시작 위치</param>
    /// <param name="end">도착 위치</param>
    /// <returns>시작 위치에서 도착위치 까지의 경로 (길을 못찾으면 null)</returns>
    public static List<Vector2Int> PathFind(GridMap map, Vector2Int start, Vector2Int end)
    {
        List<Vector2Int> path = null;

        List<Node> openList = new List<Node>();
        List<Node> closeList = new List<Node>();

        Node current = map.GetNode(start);
        closeList.Add(current);
        path.Add(start);



        // 주위 노드를 openlist 에 넣는다
        Vector2Int currentGrid = new(current.X, current.Y);

        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (i == 0 && j == 0) continue;

                Vector2Int nodeGrid = new(current.X + i, current.Y + j);
                
                if (map.IsPlain(nodeGrid))
                {
                    
                    openList.Add(map.GetNode(nodeGrid));
                }
            }
        }
        // 주위 노드의 f(x) 값을 구한다

        return path;
    }

    

    // A* 알고리즘의 휴리스틱 값 계산하는 함수 ( 현재 위치에서 목적지 까지의 예상거리 )
    private static float GetHeuristic(Node current, Vector2Int end)
    {

        return 0;
    }
}
