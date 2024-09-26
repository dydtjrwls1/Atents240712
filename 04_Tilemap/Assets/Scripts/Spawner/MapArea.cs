using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapArea : MonoBehaviour
{
    Tilemap background;
    Tilemap obstacle;

    TileGridMap gridMap;

    public TileGridMap GridMap => gridMap;

    private void Awake()
    {
        Transform parent = transform.parent;
        Transform sibling = parent.GetChild(0);
        background = sibling.GetComponent<Tilemap>();

        sibling = parent.GetChild(1);
        obstacle = sibling.GetComponent<Tilemap>();

        gridMap = new TileGridMap(background, obstacle);
    }

    // 스폰 가능한 영역을 미리 계산하는 함수
    public List<Node> CalcSpawnArea(Vector3 position, Vector3 size)
    {
        List<Node> result = new List<Node>();

        Vector2Int min = gridMap.WorldToGrid(position);
        Vector2Int max = gridMap.WorldToGrid(position + size);

        for(int y = min.y; y < max.y; y++)
        {
            for(int x = min.x; x < max.x; x++)
            {
                if(gridMap.IsPlain(x, y))
                    result.Add(gridMap.GetNode(x, y));
            }
        }

        return result;
    }

    public Vector2 GridToWorld(int x, int y)
    {
        return gridMap.GridToWorld(new(x, y));
    }
}
