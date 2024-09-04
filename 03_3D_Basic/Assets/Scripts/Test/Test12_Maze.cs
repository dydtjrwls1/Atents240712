using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class Test12_Maze : TestBase
{
    public CellVisualize cell;

    public PathDirection direction;

    public CornerMask cornerMask;

    public PathDirection pathCheck;

    public GameObject cellPrefab;

    public MazeVisualize visualize;

    public MazeBuilder builder;

    // 윌슨 알고리즘 (미로 생성 알고리즘)
    // 1. 필드의 한 곳을 랜덤으로 미로에 추가한다.
    // 2. 미로에 포함되지 않은 필드의 한 곳을 랜덤으로 결정한다. (A셀)
    // 3. A셀 위치에서 랜덤으로 한칸 움직인다. (B셀)
    // 3.1. 움직일 때는 어느 셀에서 어느 셀로 움직였는지를 기록한다.
    // 3.2. 한 칸 움직인 곳이 미로에 포함된 셀이 아니면 3번 항목을 반복한다.
    // 4. B셀이 미로에 포함된 셀에 도착하면 B셀 시작에서 현재 셀 까지의 경로를 미로에 포함시킨다.
    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        cell.RefreshWall(direction);
        cell.RefreshCorner(cornerMask);
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        //Debug.Log(cell.CurrentActivate);
    }

    protected override void Test3_performed(InputAction.CallbackContext context)
    {
        CellBase test = new CellBase(1, 1);
        test.MakePath(direction);

        Debug.Log(test.IsPath(pathCheck));
    }

    protected override void Test4_performed(InputAction.CallbackContext context)
    {
        MazeBase maze = new WilsonMaze(5, 5, seed);
        visualize.Clear();
        visualize.Draw(maze);
    }

    protected override void Test5_performed(InputAction.CallbackContext context)
    {
        builder.Build();
    }
}
