using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGridMap : GridMap
{
    // 맵의 원점
    Vector2Int origin;

    // 이동 가능한 지역(평지)의 그리드값 배열
    Vector2Int[] movablePositions;

    // 배경 타일맵
    Tilemap background;

    // 타일맵을 이용해 그리드맵을 생성하는 생성자
    public TileGridMap(Tilemap background, Tilemap obstacle)
    {
        this.background = background;

        width = background.size.x;
        height = background.size.y;

        origin = (Vector2Int)background.origin;

        nodes = new Node[width * height];

        Vector2Int min = (Vector2Int)background.cellBounds.min;
        Vector2Int max = (Vector2Int)background.cellBounds.max;
        List<Vector2Int> movable = new List<Vector2Int>(width * height); // 이동가능한 지역을 임시로 기록할 리스트

        for (int y = min.y; y < max.y; y++)
        {
            for (int x = min.x; x < max.x; x++)
            {
                Node.NodeType nodeType = Node.NodeType.Plain; // 기본 노드 타입
                TileBase tile = obstacle.GetTile(new(x, y)); // 장애물 타일맵에서 타일 가져오기 시도
                if(tile != null)
                {
                    nodeType = Node.NodeType.Wall; // 있으면 벽
                }
                else
                {
                    movable.Add(new Vector2Int(x, y)); // 없으면 평지이므로 movable 리스트에 추가
                }

                nodes[CalcIndex(x, y)] = new Node(x, y, nodeType);
            }
        }

        movablePositions = movable.ToArray();
    }

    protected override int CalcIndex(int x, int y)
    {
        // 원점이 변경 됨 : (x - origin.x) + (y - origin.y) * width;
        // y축이 반대 : (x - origin.x) + ((height - 1) - (y - origin.y)) * width;
        
        return (x - origin.x) + ((height - 1) - (y - origin.y)) * width;
    }

    public override bool IsValidPosition(int x, int y)
    {
        return x < (width + origin.x) && y < (height + origin.y) && x >= origin.x && y >= origin.y;
    }

    // 월드 좌표를 그리드 좌표로 변경해주는 함수
    public Vector2Int WorldToGrid(Vector3 world)
    {
        return (Vector2Int)background.WorldToCell(world);
    }

    // 그리드 좌표를 월드좌표로 변경하는 함수
    public Vector2 GridToWorld(Vector2Int grid)
    {
        return background.CellToWorld((Vector3Int)grid) + new Vector3(0.5f, 0.5f); // CellToWorld 는 셀의 왼족 아래의 월드좌표를 리턴한다.
    }

    // 이동 가능한 위치중 랜덤으로 반환하는 함수
    public Vector2Int GetRandomMovablePosition()
    {
        int index = Random.Range(0, movablePositions.Length);
        return movablePositions[index]; 
    }

    // 월드 좌표를 통해 해당 위치에 있는 노드를 리턴하는 함수
    public Node GetNode(Vector3 world)
    {
        return GetNode(WorldToGrid(world));
    }

#if UNITY_EDITOR
    public int TestCalcIndex(int x, int y) 
    {
        return CalcIndex(x, y);
    }
#endif
}
