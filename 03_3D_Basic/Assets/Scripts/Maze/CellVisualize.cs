using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CellVisualize : MonoBehaviour
{
    // 셀 한 변의 길이를 나타내는 상수
    public const float CellSize = 10.0f;

    GameObject[] walls;                 // 벽 게임 오브젝트의 배열
    GameObject[] corners;               // 코너 게임 오브젝트의 배열

    public PathDirection CurrentActivate
    {
        get
        {
            int mask = 0;

            //foreach (var wall in walls)
            //{
            //    result = !wall.activeSelf ? (result | mask) : result;
            //    mask <<= 1;
            //}

            for (int i = 0; i < walls.Length; i++)
            {
                if (!walls[i].activeSelf)
                {
                    mask |= 1 << i;
                }
            }

            int result = mask;

            return (PathDirection)result;
        }
    }

    private void Awake()
    {
        Transform child = transform.GetChild(1);

        walls = new GameObject[child.childCount];
        for(int i = 0; i < walls.Length; i++)
        {
            walls[i] = child.GetChild(i).gameObject;
        }

        child = transform.GetChild(2);
        corners = new GameObject[child.childCount];
        for (int i = 0;i < corners.Length; i++)
        {
            corners[i] = child.GetChild(i).gameObject;
        }
    }

    /// <summary>
    /// 입력받은 데이터에 맞게 벽의 활성화 여부 재설정
    /// </summary>
    /// <param name="data">벽의 On/Off 를 표시하는 비트마스크</param>
    public void RefreshWall(PathDirection flagData)
    {
        // 북동남서 순서대로 1이 세팅되어 있으면 길 (=벽 없음), 0 이 세팅되어 있으면 벽
        // 0000 ~ 1111
        // 0001 : 북쪽에만 길이있고 나머지는 벽이다.
        int flag = (int)flagData;

        for(int i = 0; i < walls.Length; i++)
        {
            int mask = 1 << i;
            bool activate = (mask & flag) == 0;
            walls[i].SetActive(activate);
        }
    }

    public void RefreshCorner(CornerMask flagData)
    {
        int flag = (int)flagData;

        for (int i = 0; i < corners.Length; i++)
        {
            int mask = 1 << i;
            bool activate = (mask & flag) == 0;
            corners[i].SetActive(activate);
        }
    }

}
