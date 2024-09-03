using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeVisualize : MonoBehaviour
{
    public GameObject cellPrefab;

    public int width = 10;
    public int height = 10;

    WilsonMaze maze;

    GameObject[] grid;

    private void Awake()
    {
        grid = new GameObject[width * height];
        maze = new WilsonMaze(width, height);
    }

    private void Start()
    {
        // 셀을 알맞은 위치에 배치하고 배열에 넣는다.
        for(int i = 0;  i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 pos = Vector3.right * i * 10.0f + Vector3.forward * j * -10.0f;
                GameObject cell = Instantiate(cellPrefab, pos, Quaternion.identity, transform);
                grid[i * width + j] = cell;
            }
        }

        var cells = maze.Cells;

        for (int i = 0; i < cells.Length; i++)
        {
            CellVisualize visual = grid[i].GetComponent<CellVisualize>();
            visual.RefreshWall(cells[i].Path);
        }
    }
}
