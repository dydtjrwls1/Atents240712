using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MazeVisualize : MonoBehaviour
{
    public GameObject cellPrefab;

    MazeBase maze = null;

    // 코너 방향 세트를 저장해 놓은 배열(북서, 북동, 남동, 남서 순서)
    (PathDirection, PathDirection)[] corners = null;

    // 이웃의 방향을 저장해 놓은 딕셔너리
    Dictionary<PathDirection, Vector2Int> neighborDir;

    private void Awake()
    {
        corners = new (PathDirection, PathDirection)[]
        {
            (PathDirection.North, PathDirection.West),
            (PathDirection.North, PathDirection.East),
            (PathDirection.South, PathDirection.East),
            (PathDirection.South, PathDirection.West),
        };

        neighborDir = new Dictionary<PathDirection, Vector2Int>(4);

        neighborDir[PathDirection.North] = new Vector2Int(0, -1);
        neighborDir[PathDirection.South] = new Vector2Int(0, 1);
        neighborDir[PathDirection.East] = new Vector2Int(1, 0);
        neighborDir[PathDirection.West] = new Vector2Int(-1, 0);
    }

    public void Draw(MazeBase maze)
    {
        this.maze = maze;
        float size = CellVisualize.CellSize;

        // 셀을 알맞은 위치에 배치하고 배열에 넣는다.
        foreach (var cell in maze.Cells)
        {
            GameObject obj = Instantiate(cellPrefab, transform);
            obj.transform.Translate(cell.X * size, 0, -cell.Y * size);
            obj.name = $"Cell_({cell.X}, {cell.Y})";

            CellVisualize vis = obj.GetComponent<CellVisualize>();
            vis.RefreshWall(cell.Path);

            // 코너 : 모서리쪽 이웃으로 길이 있고, 이웃이 내 모서리쪽에 벽이 있다.
            CornerMask cornerFlag = 0;
            for(int i = 0; i < corners.Length; i++)
            {
                if(IsCornerVisible(cell, corners[i].Item1, corners[i].Item2))
                {
                    cornerFlag |= (CornerMask)(1 << i);
                }
            }

            vis.RefreshCorner(cornerFlag);
        }
    }

    public void Clear()
    {
        while(transform.childCount > 0) // 자식이 남아있는 한 반복
        {
            Transform child = transform.GetChild(0); // 첫번째 자식을 선택
            child.SetParent(null);                   // 부모에서 떼어내기
            Destroy(child.gameObject);               // 삭제(Destroy는 즉시 처리되지 않는다)
        }
    }

    bool IsCornerVisible(CellBase cell, PathDirection dir1, PathDirection dir2)
    {
        bool result = false;
        if(cell.CornerPathCheck(dir1, dir2))
        {
            CellBase neighborCell1 = maze.GetCell(cell.X + neighborDir[dir1].x, cell.Y + neighborDir[dir1].y);
            CellBase neighborCell2 = maze.GetCell(cell.X + neighborDir[dir2].x, cell.Y + neighborDir[dir2].y);

            result = neighborCell1.IsWall(dir2) && neighborCell2.IsWall(dir1);
        }

        return result;
    }

    /// <summary>
    /// 그리드 좌표로 셀의 로컬 좌표 구하는 함수
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Vector3 GridToLocal(int x, int y)
    {
        float size = CellVisualize.CellSize;
        float sizeHalf = size * 0.5f;

        return new(size * x + sizeHalf, 0, size * y - sizeHalf);
    }

    /// <summary>
    /// 로컬 좌표를 그리드 좌표로 변경하는 함수
    /// </summary>
    /// <param name="local"></param>
    /// <returns></returns>
    public static Vector2Int LocalToGrid(Vector3 local)
    {
        float size = CellVisualize.CellSize;

        return new((int)(local.x / size), (int)(-local.z / size));
    }
}
