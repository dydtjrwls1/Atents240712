
using System;
using UnityEngine;

public class Node : IComparable<Node>
{
    // 그리드 맵에서의 X, Y 좌표
    private int x;
    private int y;

    public int X => x;
    public int Y => y;

    // 시작 노드에서 이 노드까지의 실제 이동 거리
    public float G;
    // 이 노드에서 도착 노드까지의 예상 거리
    public float H;
    public float F => G + H;

    // 노드의 종류
    public enum NodeType
    {
        Plain = 0, // 평지(이동 가능)
        Wall,      // 벽(이동 불가능)
        Slime,     // 슬라임(이동 불가능)
    }

    public NodeType nodeType = NodeType.Plain;

    // 경로 상 앞에 있는 노드
    public Node prev;

    // Node 생성자
    public Node(int x, int y, NodeType nodeType = NodeType.Plain)
    {
        this.x = x;
        this.y = y;
        this.nodeType = nodeType;
    }

    // 초기화용 함수 (길찾기 반복할 때 초기화 용도)
    public void ClearData()
    {
        G = float.MaxValue;
        H = float.MaxValue;
        prev = null;
    }

    // 같은 타입간의 크기를 비교하는 함수
    // -1 : 비교 대상보다 작다
    // 0 : 비교 대상과 같다
    // 1 : 비교 대상보다 크다
    public int CompareTo(Node other)
    {
        if(other == null) return -1;  // other 가 null 이면 this가 더 작다(작은 순서대로 정렬하는게 목적이다)

        return F.CompareTo(other.F); // F 값을 기준으로 순서를 정해라
    }

    public static bool operator ==(Node left, Vector2Int right)
    {
        return left.X == right.x && left.Y == right.y;
    }

    public static bool operator !=(Node left, Vector2Int right)
    {
        return left.X != right.x || left.Y != right.y;
    }

    public override bool Equals(object obj)
    {
        return obj is Node other && this.x == other.x && this.y == other.y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.x, this.y); // x, y 위치 값을 조합해서 해시코드 만들기
    }
}
