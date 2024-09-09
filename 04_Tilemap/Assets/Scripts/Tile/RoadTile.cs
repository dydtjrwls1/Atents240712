using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class RoadTile : Tile
{
    [Flags]
    enum AdjTilePosition : byte
    {
        None = 0,
        North = 1,
        East = 2,
        South = 4,
        West = 8,
        All = North | East | South | West
    }

    // 길을 구정하는 스프라이트들
    public Sprite[] sprites;

    /// <summary>
    /// 타일이 그려질 때 자동으로 호출되는 함수
    /// </summary>
    /// <param name="position">타일의 위치</param>
    /// <param name="tilemap">이 타일이 그려지는 타일맵</param>
    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        // 주변에 있는 같은 종류의 타일 갱신하기
        for(int y = -1; y < 2; y++)
        {
            for(int x = -1; x < 2; x++)
            {
                Vector3Int location = new Vector3Int(position.x + x, position.y + y, position.z); // position 주변 위치 구하기
                if(HasThisType(tilemap, location))
                {
                    tilemap.RefreshTile(location); // 같은 타일일 때만 갱신
                }
            }
        }
    }

    /// <summary>
    /// 타일맵의 RefreshTile함수가 호출될 때 호출, 타일이 어떤 스프라이트를 그릴지 결정하는 함수
    /// </summary>
    /// <param name="position">타일 데이터를 가져올 타일의 위치</param>
    /// <param name="tilemap">타일 데이터를 가져올 타일맵</param>
    /// <param name="tileData">가져온 타일 데이터의 참조(읽기쓰기 둘 다 가능)</param>
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        // 주변 타일정보를 확인해서 스프라이트 선택
        AdjTilePosition mask = AdjTilePosition.None;

        mask |= HasThisType(tilemap, position + new Vector3Int(0, 1, 0)) ? AdjTilePosition.North : AdjTilePosition.None;
        mask |= HasThisType(tilemap, position + new Vector3Int(1, 0, 0)) ? AdjTilePosition.East : AdjTilePosition.None;
        mask |= HasThisType(tilemap, position + new Vector3Int(0, -1, 0)) ? AdjTilePosition.South : AdjTilePosition.None;
        mask |= HasThisType(tilemap, position + new Vector3Int(-1, 0, 0)) ? AdjTilePosition.West : AdjTilePosition.None;

        int index = GetIndex(mask);
        if (index > -1 && index < sprites.Length)
        {
            tileData.sprite = sprites[index];
            Matrix4x4 matrix = tileData.transform;
            matrix.SetTRS(Vector3.zero, GetRotation(mask), Vector3.one); // 로컬값으로 넣어줌 (회전만 변경)
            tileData.transform = matrix;
            tileData.flags = TileFlags.LockTransform; // 다른 타일이 회전을 못시키게 잠금
        }
        else
        {
            Debug.LogError($"잘못된 인덱스 : {index}, mask = {mask}");
        }
    }

    /// <summary>
    /// 특정 타일맵의 특정 위치에 이 타일과 같은 종류의 타일이 있는지 확인하는 함수
    /// </summary>
    /// <param name="tilemap">확인할 타일맵</param>
    /// <param name="position">확인할 위치</param>
    /// <returns>true => 같은 종류 | false => 다른 종류</returns>
    bool HasThisType(ITilemap tilemap, Vector3Int position)
    {
        return tilemap.GetTile(position) == this; // 같은 객체(종류)인지 확인
    }

    /// <summary>
    /// 마스크값에 따라 그릴 스프라이트의 인덱스를 리턴하는 함수
    /// </summary>
    /// <param name="mask"></param>
    /// <returns></returns>
    int GetIndex(AdjTilePosition mask)
    {
        int index = 0;

        switch (mask)
        {
            case AdjTilePosition.North | AdjTilePosition.East:
            case AdjTilePosition.South | AdjTilePosition.West:
            case AdjTilePosition.North | AdjTilePosition.West:
            case AdjTilePosition.South | AdjTilePosition.East:
                index = 1; // ㄱ 자 모양
                break;
            case AdjTilePosition.All & ~AdjTilePosition.North:
            case AdjTilePosition.All & ~AdjTilePosition.South:
            case AdjTilePosition.All & ~AdjTilePosition.West:
            case AdjTilePosition.All & ~AdjTilePosition.East:
                index = 2; // ㅗ 자 모양
                break;
            case AdjTilePosition.All:
                index = 3; // + 자 모양
                break;
            case AdjTilePosition.None:
            case AdjTilePosition.North:
            case AdjTilePosition.East:
            case AdjTilePosition.West:
            case AdjTilePosition.South:
            case AdjTilePosition.North | AdjTilePosition.South:
            case AdjTilePosition.East | AdjTilePosition.West:
                index = 0; // 1 자 모양
                break;
            default:
                index = 0;
                break;
        }

        return index;
    }

    /// <summary>
    /// 마스크 값에 따라 스프라이트를 얼마나 회전시킬 것인지 결정하는 함수
    /// </summary>
    /// <param name="mask">주변 타일 정보</param>
    /// <returns></returns>
    Quaternion GetRotation(AdjTilePosition mask)
    {
        Quaternion rotate = Quaternion.identity;

        switch ((mask)
)
        {
            case AdjTilePosition.East:
            case AdjTilePosition.West:
            case AdjTilePosition.East | AdjTilePosition.West:
            case AdjTilePosition.North | AdjTilePosition.West:
            case AdjTilePosition.All & ~AdjTilePosition.West:
                rotate = Quaternion.Euler(0, 0, -90);
                break;
            case AdjTilePosition.North | AdjTilePosition.East:
            case AdjTilePosition.All & ~AdjTilePosition.North:
                rotate = Quaternion.Euler(0, 0, -180);
                break;
            case AdjTilePosition.South | AdjTilePosition.East:
            case AdjTilePosition.All & ~AdjTilePosition.East:
                rotate = Quaternion.Euler(0, 0, -270);
                break;
        }
        return rotate;
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/2D/Tiles/Custom/RoadTile")]
    public static void CreateRoadTile() // 메뉴가 클릭되었을 때 실행될 함수
    {
        // 파일 저장 창을 열어서 풀 경로 받기
        string path = EditorUtility.SaveFilePanelInProject(
            "Save Road Tile",
            "new Road Tile",
            "Asset",
            "Save Road Tile",
            "Assets/Tiles"
            );

        if(path != string.Empty)
        {
            AssetDatabase.CreateAsset(CreateInstance<RoadTile>(), path);
        }
    }

#endif
}