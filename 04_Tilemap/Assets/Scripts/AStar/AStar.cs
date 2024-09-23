using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public static class AStar
{
    // 옆으로 이동하는 거리
    const float sideDistance = 1.0f;
    //const float sideDistance = 10f;

    // 대각선으로 이동하는 거리
    const float diagonalDistance = 1.4142135f;
    //const float diagonalDistance = 14f;

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

        // 시작 위치와 도착 위치가 맵 안이고 벽이 아닐 때 실행
        if(map.IsValidPosition(start) && map.IsValidPosition(end) && !map.IsWall(start) && !map.IsWall(end))
        {
            map.ClearMapData();

            List<Node> open = new List<Node>(8);
            List<Node> close = new List<Node>(8);

            // 시작 Node 를 Open 리스트에 넣고 F 값을 구한다.
            Node current = map.GetNode(start);
            current.G = 0;
            current.H = GetHeuristic(current, end);
            open.Add(current);

            // A* 루프 시작
            while(open.Count > 0) // 탐색에 실패했을 경우 (=open 리스트에 더 이상 노드가 남아 있지않을 경우)
            {
                open.Sort();            // F 값을 기준으로 정렬
                current = open[0];      // F 값을 기준으로 정렬했기 때문에 제일 앞에 있는것이 F값이 가장 작다
                open.RemoveAt(0);

                if(current != end)
                {
                    // 목적지가 아니다
                    close.Add(current);

                    // current 의 주변 유효한 노드를 open 리스트에 넣는다.
                    for(int y = -1; y < 2; y++)
                    {
                        for(int x = -1; x < 2; x++)
                        {
                            Node node = map.GetNode(current.X + x, current.Y + y);

                            // 스킵할 노드 확인 ( 맵 범위 밖 | 현재 노드일 경우 | 노드 타입이 벽 | close 리스트에 이미 있을경우 | 대각선 이동중 옆에 벽이 있을 경우 )
                            if(node == null || 
                               node == current ||
                               node.nodeType == Node.NodeType.Wall || 
                               close.Contains(node))  // close.Exists((x) => x == node) 와 같다.
                                continue;

                            bool isDiagonal = (x * y) != 0;
                            if (isDiagonal &&
                                (map.IsWall(current.X + x, current.Y) || map.IsWall(current.X, current.Y + y)))
                                continue; // 대각선이고 한쪽이 벽이면 스킵

                            // 이동 거리 (= G 값)
                            float distance = isDiagonal ? diagonalDistance : sideDistance; 

                            // node 는 이미 open 리스트에 있거나 어느 리스트에도 들어가지 않았다.
                            if(node.G > current.G + distance) // 노드가 가진 G값이 current 를 거쳐서 이동한 것보다 크다
                            {
                                if(node.prev == null) // 아직 open 리스트에 들어간 적 없다.
                                {
                                    node.H = GetHeuristic(node, end); // 휴리스틱 값 계산
                                    open.Add(node);
                                }

                                // 이전에 open 리스트에 들어간 적 있다.
                                // 기존 G 값의 갱신이 필요하다.
                                node.G = current.G + distance;
                                node.prev = current;
                            }
                        }
                    }
                }
                else
                {
                    break; // 목적지에 도착했음 (while 탈출)
                }
            }

            // 마무리 작업 (목적지에 도착 했을 때만)
            if(current == end)
            {
                path = new List<Vector2Int>();

                Node result = current;
                while(result != null) // result 가 null이 될 때 까지
                {
                    path.Add(new Vector2Int(result.X, result.Y)); // current 위치 추가(역으로)
                    result = result.prev;
                }

                path.Reverse();         // 도착지점에서 시작지점까지 역으로 경로가 들어있던 것을 뒤집기
            }
        } 

        return path;
    }

    

    // A* 알고리즘의 휴리스틱 값 계산하는 함수 ( 현재 위치에서 목적지 까지의 예상거리 )
    private static float GetHeuristic(Node current, Vector2Int end)
    {
        return Mathf.Abs(current.X - end.x) + Mathf.Abs(current.Y - end.y);
    }
}
