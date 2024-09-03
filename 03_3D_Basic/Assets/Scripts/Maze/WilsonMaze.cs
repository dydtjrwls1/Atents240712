using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class WilsonMaze : MazeBase
{
    // 이웃의 방향을 저장해놓은 변수
    readonly Vector2Int[] dirs = { new(0, 1), new(0, -1), new(-1, 0), new(1, 0) };

    public WilsonMaze(int width, int height, int seed = -1) : base(width, height, seed)
    {
    }

    protected override void OnSpecificAlgotirhmExcute()
    {
        // 1. 필드의 한 곳을 랜덤으로 미로에 추가한다.
        // 2. 미로에 포함되지 않은 셀 중 하나를 랜덤으로 선택한다.(A셀)
        // 3. A셀 위치에서 랜덤으로 한 칸씩 움직인다. (움직인 경로는 기록되어야 한다)
        // 4. 미로에 포함된 셀에 도착할 때 까지 3번을 반복한다.
        // 5. A셀 위치에서 미로에 포함된 영역에 도착할 때 까지의 경로를 미로에 포함시킨다.(경로에 따라 벽을 제거한다.)
        // 6. 모든 셀이 미로에 포함될 때 까지 2번으로 돌아가 반복한다.

        // 우선 셀 생성
        for (int y = 0 ; y < height; y++)
        {
            for (int x = 0 ; x < width; x++)
            {
                cells[GridToIndex(x, y)] = new WilsonCell(x, y);
            }
        }

        // 미로에 포함되지 않은 셀의 리스트 만들기 (+ 랜덤으로 섞는다)
        int[] notInMazeArray = new int[cells.Length];
        for (int i = 0; i < notInMazeArray.Length; i++)
        {
            notInMazeArray[i] = i;
        }

        Util.Shuffle(notInMazeArray); // 미로에 포함되지 않은 array를 한번 섞고 List 로 만들기
        
        List<int> notInMaze = new List<int>(notInMazeArray);

        // 1. 필드의 한 곳을 랜덤으로 미로에 추가 한다.
        int firstIndex = notInMaze[0];
        notInMaze.RemoveAt(0);

        WilsonCell first = cells[firstIndex] as WilsonCell;
        first.isMazeMember = true;

        
        while (notInMaze.Count > 0)
        {
            // 2. 미로에 포함되지 않은 셀 중 하나를 랜덤으로 선택한다.(A셀)
            int index = notInMaze[0];
            notInMaze.RemoveAt(0);
            WilsonCell current = cells[index] as WilsonCell;

            // 3. A셀 위치에서 랜덤으로 한 칸씩 움직인다. (움직인 경로는 기록되어야 한다)
            while (!current.isMazeMember)
            {
                WilsonCell neighbor = GetNeighbor(current) as WilsonCell; // 이웃 구하고
                current.next = neighbor; // 경로 저장
                current = neighbor; // 이웃을 새 current 로 지정
            }

            // 5. A셀 위치에서 미로에 포함된 영역에 도착할 때 까지의 경로를 미로에 포함시킨다.(경로에 따라 벽을 제거한다.)
            WilsonCell path = cells[index] as WilsonCell;
            while (path != current)
            {
                path.isMazeMember = true;                      // 이 셀을 미로에 포함시킨다
                notInMaze.Remove(GridToIndex(path.X, path.Y)); // notInMaze 리스트에서 현재 셀 삭제
                ConnectPath(path, path.next);                  // 현재 셀과 다음 셀로 이동
                path = path.next;                              // 다음 경로로 이동
            }

            path.isMazeMember = true;                      // 이 셀을 미로에 포함시킨다
            notInMaze.Remove(GridToIndex(path.X, path.Y)); // notInMaze 리스트에서 현재 셀 삭제

            // 6. 모든 셀이 미로에 포함될 때 까지 2번으로 돌아가 반복한다.
        }
    }

    // 파라메터로 받은 셀의 이웃 중 하나를 리턴하는 함수
    CellBase GetNeighbor(WilsonCell cell)
    {
        Vector2Int neighborPos;

        do
        {
            Vector2Int dir = dirs[Random.Range(0, dirs.Length)];
            neighborPos = new(cell.X + dir.x, cell.Y + dir.y);
        } while (!IsInGrid(neighborPos)); // 그리드 영역 안이 선택될 때 까지 반복

        return cells[GridToIndex(neighborPos)];
    }
}
