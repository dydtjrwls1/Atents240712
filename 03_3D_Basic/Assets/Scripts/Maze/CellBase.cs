// 미로의 한 칸을 표시하는 클래스
public class CellBase
{
    // 이 셀에 있는 길을 기록하는 변수(북동남서 순서대로 비트 설정)
    PathDirection path;
    
    // 열린 길을 확인하기 위한 프로퍼티
    public PathDirection Path => path;

    // 미로 그리드 상에서의 x 좌표(왼 => 오)
    protected int x;
    // 미로 그리드 상에서의 y 좌표(위 => 아래)
    protected int y;

    // 좌표 확인용 프로퍼티
    public int X => x;
    public int Y => y;

    // x,y 좌표를 받는 생성자
    public CellBase(int x, int y)
    {
        path = PathDirection.None;
        this.x = x;
        this.y = y;
    }

    // 이 셀에 길을 새로 추가하는 함수
    public void MakePath(PathDirection newPath)
    {
        path |= newPath;
    }

    // 특정 방향이 길인지 확인하는 함수
    public bool IsPath(PathDirection direction)
    {
        // true 면 길이다. falst 면 벽이다.
        return (Path & direction) != 0;
    }

    // 특정 방향이 벽인지 확인하는 함수
    public bool IsWall(PathDirection direction)
    {
        // true 면 벽이다. falst 면 길이다.
        return (Path & direction) == 0;
    }

    /// <summary>
    /// 코너 체그용 함수
    /// </summary>
    /// <param name="dir1">확인할 방향1</param>
    /// <param name="dir2">확인할 방향2</param>
    /// <returns>dir1, dir2가 코너를 만드는 방향이고 둘 다 길이 있으면 true</returns>
    public bool CornerPathCheck(PathDirection dir1, PathDirection dir2)
    {
        bool result = false;
        PathDirection corner = dir1 | dir2;
        if (corner == (PathDirection.North | PathDirection.West)
            || corner == (PathDirection.North | PathDirection.East)
            || corner == (PathDirection.South | PathDirection.East)
            || corner == (PathDirection.South | PathDirection.West))
        {
            result = IsPath(dir1) && IsPath(dir2);
        }

        return result;
    }
}
