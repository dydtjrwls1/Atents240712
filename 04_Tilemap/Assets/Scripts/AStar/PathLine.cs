using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLine : MonoBehaviour
{
    LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.startColor = new Color(Random.value, 0, Random.value);
        lineRenderer.endColor = lineRenderer.startColor;
        ClearPath();
    }

    public void DrawPath(TileGridMap map, List<Vector2Int> path)
    {
        if(map != null && path != null)
        {
            lineRenderer.positionCount = path.Count; // 경로 개수만큼 위치 추가
            
            int index = 0;
            foreach(Vector2Int p in path)
            {
                Vector2 world = map.GridToWorld(p);     // 경로를 월드좌표로 변환
                lineRenderer.SetPosition(index, world); // 경로 추가
                index++;
            }
        }
        else
        {
            ClearPath();
        }
    }

    public void ClearPath()
    {
        if(lineRenderer != null)
        {
            lineRenderer.positionCount = 0; // 초기화
        }
    }
}
