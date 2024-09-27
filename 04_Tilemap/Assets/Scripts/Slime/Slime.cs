using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Apple;

public class Slime : RecycleObject
{
    Material material;

    // 슬라임이 움직일 그리드 맵
    TileGridMap map;

    // 슬라임이 이동할 경로를 그려줄 객체
    PathLine pathLine;

    SpriteRenderer spriteRenderer;

    // 이 슬라임이 위치한 노드
    Node current = null;

    // 이 슬라임이 생성 된 풀
    Transform pool;

    // 슬라임이 이동할 경로
    List<Vector2Int> path;

    // 현재 슬라임의 위치를 그리드 좌표로 알려주는 프로퍼티
    Vector2Int GridPosition => map.WorldToGrid(transform.position);

    // 이동 경로를 보여줄지 결정하는 변수
    bool isShowPath = false;

    // 슬라임 이동 활성화 변수(true => 이동 | false => 정지)
    bool isMoveActivate = false;

    float pathWaitTime = 0.0f;

    public Action onDie = null;

    // pool 에 단 한번만 값을 설정하는 프로퍼티
    Node Current
    {
        get => current;
        set
        {
            if(current != value) // current 에 변화가 있을 때
            {
                if(current != null) // current에 이미 다른 노드가 할당 되어 있다면
                    current.nodeType = Node.NodeType.Plain; // 노드를 Plain으로 변경한다

                current = value; // current 를 새 노드로 바꾼다

                if(current != null) // 새 노드가 null 이 아니라면
                    current.nodeType = Node.NodeType.Slime; // 노드 타입을 슬라임으로 바꾼다.
            }
        }
    }

    // 이동 속도
    public float moveSpeed = 2.0f;

    // 다른 슬라임으로 인해 경로가 막혔을 경우 최대 대기 시간
    public float maxPathWaitTime = 1.0f;

    [Range(0f, 1f)]
    public float outlineThickness = 0.005f;

    [Range(0f, 1f)]
    public float phaseThickness = 0.01f;

    public float phaseDuration = 0.5f;
    public float dissolveDuration = 1.0f;

    readonly int OutlineThickness_Hash = Shader.PropertyToID("_OutlineThickness");
    readonly int PhaseSplit_Hash = Shader.PropertyToID("_PhaseSplit");
    readonly int PhaseThickness_Hash = Shader.PropertyToID("_PhaseThickness");
    readonly int DissolveFade_Hash = Shader.PropertyToID("_DissolveFade");

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
        pathLine = GetComponentInChildren<PathLine>();
        path = new List<Vector2Int>();

        pool = transform.parent;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    private void Update()
    {
        MoveUpdate();
    }

    // 슬라임 초기화 함수 (풀에서 꺼낸 직후 실행한다.)
    public void Initialized(TileGridMap map, Vector3 worldPos)
    {
        this.map = map;
        transform.position = map.GridToWorld(map.WorldToGrid(worldPos)); // WorldPos가 있는 셀의 가운데 위치에 배치

        Current = map.GetNode(worldPos);
    }

    // 이동 처리 함수
    private void MoveUpdate()
    {
        if (isMoveActivate)
        {
            // path 가 있다 && path 의 개수가 1개 이상이다 && 충분히 기다리지 않았다면
            if (path != null && path.Count > 0 && pathWaitTime < maxPathWaitTime)
            {
                Vector2Int destinationGrid = path[0];

                if (!map.IsSlime(destinationGrid) || map.GetNode(destinationGrid) == Current) // 목적지가 슬라임이 아니거나 목적지 노드가 내가 있는 노드라면 이동한다.
                {
                    Vector3 destinationPos = map.GridToWorld(destinationGrid);
                    Vector3 direction = destinationPos - transform.position;

                    if (direction.sqrMagnitude < 0.001f)
                    {
                        // 도착했다
                        transform.position = destinationPos;
                        path.RemoveAt(0);
                    }
                    else
                    {
                        transform.Translate(Time.deltaTime * moveSpeed * direction.normalized);
                        Current = map.GetNode(transform.position);
                    }

                    // 아래쪽에 있는 슬라임이 위에 그려지게 하기
                    spriteRenderer.sortingOrder = -Mathf.FloorToInt(transform.position.y * 100f);

                    pathWaitTime = 0.0f;
                }
                else
                {
                    //Debug.Log($"name : {gameObject.name} Current : ({Current.X},{Current.Y}) destination : {path[0]}");
                    pathWaitTime += Time.deltaTime;
                }
            }
            else
            {
                pathWaitTime = 0.0f;
                SetDestination(map.GetRandomMovablePosition());
            }
        }
        
    }

    protected override void OnReset()
    {
        // Phase
        ShowOutline(false);
        material.SetFloat(DissolveFade_Hash, 1.0f);
        material.SetFloat(PhaseThickness_Hash, phaseThickness);
        isMoveActivate = false;
        StartCoroutine(Phase());
    }

    IEnumerator Phase()
    {
        float elapsedTime = 0.0f;
        float phaseNormalize = 1.0f / phaseDuration;

        while(elapsedTime < phaseDuration)
        {
            elapsedTime += Time.deltaTime;
            material.SetFloat(PhaseSplit_Hash, 1.0f - (elapsedTime * phaseNormalize));
            yield return null;
        }

        material.SetFloat(PhaseSplit_Hash, 0.0f);
        material.SetFloat(PhaseThickness_Hash, 0.0f);
        isMoveActivate = true;
    }

    /// <summary>
    /// Outline을 보여줄지 말지 결정하는 함수
    /// </summary>
    /// <param name="isShow">true => 보여줌 / false => 안 보여줌</param>
    public void ShowOutline(bool isShow = true)
    {
        // Outline
        float thickness = isShow ? outlineThickness : 0.0f;
        material.SetFloat(OutlineThickness_Hash, thickness);
    }

    public void Die()
    {
        // Dissolve
        onDie?.Invoke();
        isMoveActivate = false;
        StartCoroutine(Dissolve());
    }

    IEnumerator Dissolve()
    {
        float elapsedTime = 0.0f;
        float dissolveNormalize = 1.0f / dissolveDuration;

        while(elapsedTime < dissolveDuration) 
        {
            elapsedTime += Time.deltaTime;
            material.SetFloat(DissolveFade_Hash, 1.0f - (elapsedTime * dissolveNormalize));
            yield return null;
        }

        material.SetFloat(DissolveFade_Hash, 0.0f);

        ReturnToPool();
    }

    // 그리드 좌표로 슬라임의 목적지를 지정하는 함수
    public void SetDestination(Vector2Int destination)
    {
        path = AStar.PathFind(map, GridPosition, destination);
        if (isShowPath)
        {
            pathLine.DrawPath(map, path);
        }
    }

    // 월드좌표로 슬라임의 목적지를 지정하는 함수
    public void SetDestination(Vector3 destination)
    {
        Vector2Int grid = map.WorldToGrid(destination);
        if (map.IsValidPosition(grid) && map.IsPlain(grid))
        {
            SetDestination(grid);
        }
        
    }

    // 경로 시각화 결정 함수
    public void ShowPath(bool isShow = false)
    {
        isShowPath = isShow;
        if (isShowPath)
        {
            pathLine.DrawPath(map, path);
        }
        else
        {
            pathLine.ClearPath();
        }
    }

    public void ReturnToPool()
    {
        transform.SetParent(pool);      // 부모를 pool 로 재설정
        Current = null;                 // Current 비우기

        path?.Clear();                   // 경로 제거
        pathLine.ClearPath();           

        gameObject.SetActive(false);
    }
}
