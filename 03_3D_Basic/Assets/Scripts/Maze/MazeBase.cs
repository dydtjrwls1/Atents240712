using Unity.VisualScripting;
using UnityEngine;

public class MazeBase : MonoBehaviour
{
    // 미로의 가로 새로 크기
    protected int width;
    protected int height;

    // 가로 새로 확인용 프로퍼티
    public int Width => width;
    public int Height => height;

    // 미로를 구성하는 셀들
    // c# 에선 이중배열 데이터에 접근할 때 매우 느리다 (= 배열을 쓰는 의미가 없다)
    protected CellBase[] cells;

    // 셀들 확인용 프로퍼티
    public CellBase[] Cells => cells;


    /// <summary>
    /// 미로 생성자
    /// </summary>
    /// <param name="width">가로 길이</param>
    /// <param name="height">새로 길이</param>
    /// <param name="seed">시드값</param>
    public MazeBase(int width, int height, int seed = -1)
    {
        this.width = width;
        this.height = height;

        if (seed != -1)
        {
            Random.InitState(seed);                 
        }

        int size = width * height;              

        cells = new CellBase[size];

        OnSpecificAlgotirhmExcute();
    }

    /// <summary>
    /// 각 알고리즘 별로 override 해서 사용할 함수
    /// </summary>
    protected virtual void OnSpecificAlgotirhmExcute()
    {
        // 셀 생성과 알고리즘 결과에 맞게 세팅
    }

    /// <summary>
    /// from 에서 to 사이를 지날수 있는 경로 만들기
    /// </summary>
    /// <param name="from">시작 셀</param>
    /// <param name="to">도착 셀</param>
    protected void ConnectPath(CellBase from, CellBase to)
    {
        Vector2Int dir = new(to.X - from.X, to.Y - from.Y);

        if(dir.x > 0)
        {
            // 동쪽
            from.MakePath(PathDirection.East);
            to.MakePath(PathDirection.West);
        }
        else if(dir.x < 0)
        {
            // 서쪽
            from.MakePath(PathDirection.West);
            to.MakePath(PathDirection.East);
        }
        else if(dir.y > 0)
        {
            // 북쪽
            from.MakePath(PathDirection.North);
            to.MakePath(PathDirection.South);
        }
        else if(dir.y < 0)
        {
            // 남쪽
            from.MakePath(PathDirection.South);
            to.MakePath(PathDirection.North);
        }
    }

    // 미로 안인지 밖인지 확인하는 함수
    protected bool IsInGrid(int x, int y)
    {
        // true면 미로안 false 면 미로 밖이다.
        return x >= 0 && y >= 0 && (x < width) && (y < height);
    }

    protected bool IsInGrid(Vector2Int grid)
    {
        return IsInGrid(grid.x, grid.y);
    }

    // 인덱스 값을 그리드 값으로 변환해주는 함수
    protected Vector2Int IndexToGrid(int index)
    {
        int X = index & width;
        int Y = index == 0 ? 0 : Mathf.RoundToInt(index / width);

        return new Vector2Int(X, Y);
    }

    protected int GridToIndex(Vector2Int grid)
    {
        return grid.x  + grid.y * width;
    }

    protected int GridToIndex(int x, int y)
    {
        return x + y * width;
    }

    public CellBase GetCell(int x, int y)
    {
        CellBase cell = null;
        if(IsInGrid(x, y))
        {
            int index = GridToIndex(x, y);
            cell = cells[index];
        }

        return cell;
    }
}
