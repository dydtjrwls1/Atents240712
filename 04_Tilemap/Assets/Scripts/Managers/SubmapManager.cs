using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Update()
    {
        // 완료된 작업은 작업 리스트에서 제외
        foreach(int index in loadWorkComplete)
        {
            loadWork.RemoveAll((x) => x == index); // loadWork 에서 값이 index인 항목은 모두 제거
        }
        loadWorkComplete.Clear(); // 다 제거했으니 리스트 비우기

        // 들어온 요청
        foreach(int index in loadWork)
        {
            AsyncSceneLoad(index);
        }

        foreach (int index in UnloadWorkComplete)
        {
            UnloadWork.RemoveAll((x) => x == index); // unloadWork 에서 값이 index인 항목은 모두 제거
        }
        UnloadWorkComplete.Clear(); // 다 제거했으니 리스트 비우기

        foreach (int index in UnloadWork)
        {
            AsyncSceneUnload(index);
        }
    }

    // 처음 생성 시 단 한번만 실행되는 함수
    public void PreInitialize()
    {
        int mapCount = WidthCount * HeighCount;
        sceneNames = new string[mapCount];
        sceneLoadStates = new SceneLoadState[mapCount];

        for(int y = 0; y < HeighCount; y++)
        {
            for(int x = 0; x < HeighCount; x++)
            {
                int index = GetIndex(x, y);
                sceneNames[index] = $"{SceneNameBase}_{x}_{y}";
                sceneLoadStates[index] = SceneLoadState.Unload;
            }
        }
    }

    // 신이 Single 모드로 로드될 때마다 호출되는 초기화 함수
    public void Initialize()
    {
        for(int i = 0; i < sceneLoadStates.Length; i++)
        {
            sceneLoadStates[i] = SceneLoadState.Unload;
        }

        // 플레이어 관련 초기화
    }

    /// <summary>
    /// 비동기 로딩을 요청하는 함수
    /// </summary>
    void RequestAsyncSceneLoad(int x, int y)
    {
        int index = GetIndex(x, y);
        if (sceneLoadStates[index] == SceneLoadState.Unload) // 해당 서브맵이 Unload 상태일 때만 작업 리스트에 추가한다.
        {
            loadWork.Add(index);
        }
    }

    /// <summary>
    /// 로딩 해제를 요청하는 함수
    /// </summary>
    void RequestAsyncSceneUnload(int x, int y)
    {
        int index = GetIndex(x, y);
        if (sceneLoadStates[index] == SceneLoadState.Loaded) // 해당 서브맵이 Unload 상태일 때만 작업 리스트에 추가한다.
        {
            UnloadWork.Add(index);
        }
    }

    void AsyncSceneLoad(int index)
    {
        if (sceneLoadStates[index] == SceneLoadState.Unload)    // Unload 상태인 것들만
        {
            sceneLoadStates[index] = SceneLoadState.PendingLoad; // 진행 중이라고 표시

            // 씬 로딩
            AsyncOperation async = SceneManager.LoadSceneAsync(sceneNames[index], LoadSceneMode.Additive);
            async.completed += (_) =>  // 비동기 작업이 완료되면 실행되는 델리게이트에 람다 함수 추가
            {
                sceneLoadStates[index] = SceneLoadState.Loaded; // 완료되었으니 Loaded로 상태 변경
                loadWorkComplete.Add(index);                    // 완료 리스트에 추가
            };
        }
    }

    void AsyncSceneUnload(int index)
    {
        if (sceneLoadStates[index] == SceneLoadState.Loaded)    // Loaded 상태인 것들만
        {
            sceneLoadStates[index] = SceneLoadState.PendingUnload; // 해제 진행 중이라고 표시

            // 해제할 씬에 있는 슬라임을 풀로 되돌리기 (씬 언로드로 인한 삭제 방지)
            Scene scene = SceneManager.GetSceneByName(sceneNames[index]);
            if(scene.isLoaded)
            {
                GameObject[] rootObjects = scene.GetRootGameObjects(); // 루트 오브젝트 모두 찾기(부모없는 최상위 오브젝트들)
                if(rootObjects != null && rootObjects.Length > 0)
                {
                    // 서브맵은 루트 오브젝트가 Grid 하나만 있음
                    Slime[] slimes = rootObjects[0].GetComponentsInChildren<Slime>();  // 루트 오브젝트의 자손으로 있는 모든 슬라임 찾기
                    foreach(Slime slime in slimes)
                    {
                        slime.ReturnToPool();
                    }
                }
            }

            // 씬 로딩 해제
            AsyncOperation async = SceneManager.UnloadSceneAsync(sceneNames[index]);
            async.completed += (_) =>  // 비동기 작업이 완료되면 실행되는 델리게이트에 람다 함수 추가
            {
                sceneLoadStates[index] = SceneLoadState.Unload; // 해제 완료되었으니 Unload로 상태 변경
                UnloadWorkComplete.Add(index);                    // 해제 완료 리스트에 추가
            };
        }
    }

    // grid 를 index로 변경하는 함수
    int GetIndex(int x, int y)
    {
        return x + y * HeighCount;
    }

    // 월드 좌표가 어떤 맵에 속하는지 계산하는 함수
    public Vector2Int WorldToGrid(Vector3 world)
    {
        Vector2 offset = (Vector2)world - worldOrigin;
        return new Vector2Int((int)(offset.x / SubmapWidthSize), (int)(offset.y / SubmapHeightSize));
    }

    // 지정된 위치 주변 맵은 로딩요청하고 그 외의맵은 로딩해제를 요청한다
    void RefeshScenes(int subX, int subY)
    {
        // (0, 0) ~ (WidthCount, HeightCount) 사이만 범위로 설정
        int startX = Mathf.Max(0, subX - 1);
        int endX = Mathf.Min(WidthCount, subX + 2);
        int startY = Mathf.Max(0, subY - 1);
        int endY = Mathf.Min(HeighCount, subY + 2);

        List<Vector2Int> opens = new List<Vector2Int>(9);
        for(int y = startY; y < endY; y++)
        {
            for(int x  = startX; x < endX; x++)
            {
                RequestAsyncSceneLoad(x, y);        // start ~ end 안에 있는 것은 모두 로딩 요청
                opens.Add(new Vector2Int(x, y));
            }
        }

        for(int y = 0; y < HeighCount; y++)
        {
            for(int x = 0; x < WidthCount; x++)
            {
                if (!opens.Contains(new Vector2Int(x, y)))
                {
                    RequestAsyncSceneUnload(x, y);  // 로딩 요청된 씬 제외하고 모두 로드 해제
                }
            }
        }
    }

#if UNITY_EDITOR
    public void Test_LoadScene(int x, int y)
    {
        RequestAsyncSceneLoad(x, y);
    }

    public void Test_UnloadScene(int x, int y)
    {
        RequestAsyncSceneUnload(x, y);
    }

    public void Test_UnloadAll()
    {
        for(int y = 0; y < HeighCount; y++)
        {
            for(int x = 0; x < WidthCount; x++)
            {
                RequestAsyncSceneUnload(x, y);
            }
        }
    }

    public void Test_RefreshScenes(int x, int y)
    {
        RefeshScenes(x, y);
    }
#endif

}
