using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Test10_AStarTileMap : TestBase
{
    TileGridMap tileGridMap;

    public Tilemap background;
    public Tilemap obstacle;

    public PathLine pathLine;

    public Vector2Int start;
    public Vector2Int end;

    // Start is called before the first frame update
    void Start()
    {
        tileGridMap = new TileGridMap(background, obstacle);
    }

    protected override void LClick_performed(InputAction.CallbackContext context)
    {
        Vector2 screen = Mouse.current.position.ReadValue();
        Vector3 world = Camera.main.ScreenToWorldPoint(screen);

        Node node = tileGridMap.GetNode(world);
        //Debug.Log($"{node.X}, {node.Y}");
        //int index = tileGridMap.TestCalcIndex(node.X, node.Y);
        //Debug.Log(index);

        if(!tileGridMap.IsWall(node.X, node.Y))
        {
            start = tileGridMap.WorldToGrid(world);
            Debug.Log($"Start : ({node})");
        }
        
    }

    protected override void RClick_performed(InputAction.CallbackContext context)
    {
        Vector2 screen = Mouse.current.position.ReadValue();
        Vector3 world = Camera.main.ScreenToWorldPoint(screen);

        Node node = tileGridMap.GetNode(world);
        //Debug.Log($"{node.X}, {node.Y}");
        //int index = tileGridMap.TestCalcIndex(node.X, node.Y);
        //Debug.Log(index);

        if (!tileGridMap.IsWall(node.X, node.Y))
        {
            end = tileGridMap.WorldToGrid(world);

            Debug.Log($"End : ({end})");
            List<Vector2Int> path = AStar.PathFind(tileGridMap, start, end);

            pathLine.DrawPath(tileGridMap, path);
            PrintList(path);
        }
    }

    void PrintList(List<Vector2Int> list)
    {
        if(list != null)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Vector2Int p in list)
            {
                sb.Append($"{tileGridMap.GridToIndex(p)} => ");
            }

            sb.Append("End");
            Debug.Log(sb.ToString());
        }
    }
}
