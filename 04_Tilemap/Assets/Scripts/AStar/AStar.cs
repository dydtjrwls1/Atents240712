using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
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
        map.ClearMapData();

        List<Vector2Int> path = new List<Vector2Int>();

        List<Node> openList = new List<Node>();
        List<Node> closeList = new List<Node>();

        Node current = map.GetNode(start);
        closeList.Add(current);

        bool isDiagonal = true;

        while(current != end)
        {
            // 주위 노드를 openlist 에 넣는다 (Plain 이고 CloseList에 없을 경우)
            Vector2Int currentGrid = new(current.X, current.Y);

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    Vector2Int nodeGrid = new(current.X + i, current.Y + j);
                    Node node = map.GetNode(nodeGrid);

                    // Plain 이 아니거나 노드가 Grid 범위 밖일 경우거나 CloseList에 포함되어 있을경우 넘어간다.
                    if (!map.IsPlain(nodeGrid) || node == null || closeList.Contains(node))
                    {
                        isDiagonal = !isDiagonal;
                        continue;
                    }


                    float currentG = isDiagonal ? diagonalDistance : sideDistance;

                    if(currentG < node.G)
                    {
                        node.G = currentG < node.G ? currentG : node.G;
                        node.prev = current;
                    }
                    
                    node.H = GetHeuristic(node, end);

                    if (!openList.Contains(node))
                    {
                        openList.Add(map.GetNode(nodeGrid));
                    }

                    isDiagonal = !isDiagonal;
                }
            }


            openList.Sort();

            Node nextNode = openList.First();
            nextNode.prev = current;
            current = nextNode;
            openList.RemoveAt(0);
            closeList.Add(current);
        }
        
        //foreach(Node node in closeList)
        //{
        //    Debug.Log($"{node.X}, {node.Y}");
        //    Debug.Log($"F = {node.F}");
        //}

        while(current.prev != null)
        {
            Vector2Int currentPath = new(current.X, current.Y);
            path.Add(currentPath);
            current = current.prev;
        }
        

        // 주위 노드의 f(x) 값을 구한다



        return path;
    }

    

    // A* 알고리즘의 휴리스틱 값 계산하는 함수 ( 현재 위치에서 목적지 까지의 예상거리 )
    private static float GetHeuristic(Node current, Vector2Int end)
    {
        return (Mathf.Abs(end.x - current.X) + Mathf.Abs(end.y - current.Y)) * sideDistance;
    }
}
