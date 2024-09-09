using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(RoadTile))]
public class RoadTileEditor : Editor
{
    // 에디터에서 선택된 RoadTile
    RoadTile roadTile;

    private void OnEnable()
    {
        roadTile = target as RoadTile;  // 에디터에서 선택한 에셋을 RoadTile로 캐스팅해서 저장(캐스팅 안되면 Null)
    }

    /// <summary>
    /// 에디터에서 인스펙터 창 내부를 그리는 함수
    /// </summary>
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); // 원래 그리던 대로 그리기

        if(roadTile != null) // roadTile 정보에 따라 추가로 그리기
        {
            if(roadTile.sprites != null) // 처음 만들어졌을 때는 null 이므로 반드시 필요
            {
                EditorGUILayout.LabelField("Sprite Image Preview"); // 제목

                Texture2D texture;
                for (int i = 0; i < roadTile.sprites.Length; i++)
                {
                    texture = AssetPreview.GetAssetPreview(roadTile.sprites[i]);
                    if(texture != null)
                    {
                        GUILayout.Label("",  // 라벨 이름 (설정 X)
                            GUILayout.Height(64), 
                            GUILayout.Width(64)
                            );

                        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture); // 마지막으로 잡은 영역에 texture를 그린다.
                    }
                }
            }
        }
    }
}
#endif