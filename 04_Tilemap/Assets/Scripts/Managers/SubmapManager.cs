using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmapManager : MonoBehaviour
{
    // 서브맵의 가로,새로 개수
    const int WidthCount = 3;
    const int HeighCount = 3;

    // 서브맵의 가로,새로 크기
    const float SubmapWidthSize = 20.0f;
    const float SubmapHeightSize = 20.0f;

    // 월드 (모든 서브맵의 합)의 원점 (왼쪽 아래)
    readonly Vector2 worldOrigin = new Vector2(-SubmapWidthSize * WidthCount * 0.5f, -SubmapHeightSize * HeighCount * 0.5f);

    // 씬 이름 조합용 기본 문자열
    const string SceneNameBase = "Seemless";

    // 모든 씬의 이름을 저장해 놓은 배열
    string[] sceneNames;

    enum SceneLoadState : byte
    {
        Unload = 0,     // 로딩 해제 완료 상태
        PendingUnload,  // 로딩 해제 진행중인 상태
        PendingLoad,    // 로딩 진행 중인 상태
        Loaded          // 로딩 완료된 상태
    }

    // 모든 씬의 로딩 진행 상태를 저장해 놓은 배열
    SceneLoadState[] sceneLoadStates;

    // 로딩 요청이 들어온 씬의 목록
    List<int> loadWork = new List<int>();

    // 로딩이 완료된 씬의 목록
    List<int> loadWorkComplete = new List<int>();

    // 로딩 해제 요청이 들어온 씬의 목록
    List<int> UnloadWork = new List<int>();

    // 로딩 해제 완료된 씬의 목록
    List<int> UnloadWorkComplete = new List<int>();

    // sceneLoadSates 중 하나라도 Unload 상태가 아닐시 false를 리턴하는 프로퍼티
    public bool IsUnloadAll
    {
        get
        {
            bool result = true;
            foreach (SceneLoadState state in sceneLoadStates)
            {
                if(state != SceneLoadState.Unload)
                {
                    result = false;
                    break;
                }
            }
            return result; 
        }
    }

    public void PreInitialize()
    {
        
    }

    public void Initialize()
    {

    }
}
