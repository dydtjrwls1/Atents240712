
using System;
using Unity.Mathematics;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class GridMap
{
    protected Node[] nodes;

    protected int width;
    protected int height;

    public GridMap(int width, int height)
    {
        this.width = width;
        this.height = height;

        nodes = new Node[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                //if(GridToIndex(x, y, out int? index))
                //{
                //    nodes[index.Value] = new Node(x, y);
                //}
                nodes[CalcIndex(x, y)] = new Node(x, y);
            }
        }
    }

    // 상속받은 클래스에서 따로 생성자를 만들기 위해 선언
    protected GridMap() { }

    // 특정 위치의 노트타입을 확인하는 함수 (true 면 평지, false 면 평지가 아니다)
    public bool IsPlain(int x, int y)
    {
        Node node = GetNode(x, y);
        return node != null && node.nodeType == Node.NodeType.Plain;
    }

    public bool IsPlain(Vector2Int grid)
    {
        return IsPlain(grid.x, grid.y);
    }

    public bool IsWall(int x, int y)
    {
        Node node = GetNode(x, y);
        return node != null && node.nodeType == Node.NodeType.Wall;
    }

    public bool IsWall(Vector2Int grid)
    {
        return IsWall(grid.x, grid.y);
    }

    public bool IsSlime(int x, int y)
    {
        Node node = GetNode(x, y);
        return node != null && node.nodeType == Node.NodeType.Slime;
    }

    public bool IsSlime(Vector2Int grid)
    {
        return IsSlime(grid.x, grid.y);
    }

    // 모든 노드를 대상으로 A* 계산용 데이터 클리어
    public void ClearMapData()
    {
        foreach (Node node in nodes)
        {
            node.ClearData();
        }
    }

    // 특정 위치에 있는 노드를 리턴하는 함수
    public Node GetNode(int x, int y)
    {
        Node result = null;
        if (GridToIndex(x, y, out int? index))
        {
            result = nodes[index.Value];
        }

        return result;
    }

    public Node GetNode(Vector2Int grid)
    {
        return GetNode(grid.x, grid.y);
    }

    // 그리드 좌표를 인덱스 값으로 변경해주는 함수
    protected bool GridToIndex(int x, int y, out int? index)
    {
        bool result = false;
        index = null;                   // IsValidPosition 이 false 일 때를 대비하여 값 설정

        if (IsValidPosition(x, y))      // x,y 가 맵 안인지 확인
        {
            index = CalcIndex(x, y);    // 맵 안이면 index 계산
            result = true;
        }

        return result;
    }

    // 테스트 용
    public int GridToIndex(Vector2Int grid)
    {
        GridToIndex(grid.x, grid.y, out int? index);
        return index.Value;
    }

    // 인덱스를 그리드 좌표로 변경해주는 함수
    public Vector2Int IndexToGrid(int index)
    {
        return new(index % width, index / height);  
    }

    // 간단하게 인덱스를 계산하는 함수
    protected virtual int CalcIndex(int x, int y)
    {
        return x + y * width;
    }

    // 맵 안인지 확인하는 함수
    public virtual bool IsValidPosition(int x, int y)
    {
        return x < width && y < height && x > -1 && y > -1;
    }

    public bool IsValidPosition(Vector2Int grid)
    {
        return IsValidPosition(grid.x, grid.y);
    }
}
