using UnityEngine;

public class ForestGenerator : MonoBehaviour
{
    // 여러개의 나무 프리펩을 가진다.
    // forestGenerate가 True 가 되면 generateCenter 위치가 중심이고 가로가 width 새로가 height 인 사각형 영역 안에
    // treeCount 만큼의 나무를 Tree1 타입의 나무를 생성한다. (생성된 나무는 Randomize 실행)
    // 생성 영역은 Gizmo로 표시한다.


    // 나무 프리펩들(TreeTpye과 개수와 순서가 맞아야 한다)
    public GameObject[] treePrefabs;

    // 나무 종류 표시용
    public enum TreeType
    {
        Tree1,
        Tree2
    }

    // 생성할 나무의 종류
    public TreeType type = TreeType.Tree1;

    // 생성 역할을 할 bool
    public bool forestGenerate = false;

    // 리셋 역할을 할 bool
    public bool reset = false;

    // 나무 생성 영역 크기
    public float width = 10.0f;
    public float height = 10.0f;

    // 생성할 나무의 개수
    public int treeCount = 10;

    // 나무 생성 영역 중심점 설정용 트랜스폼
    Transform generateCenter;

    // 생성된 나무들의 부모가 될 transform
    Transform trees;

    [Space(10)]
    [Tooltip("일련 번호용 (리셋할 때 제외하고 수정 금지)")]
    public int serializeNumber = 0;

    private void Awake()
    {
        generateCenter = transform.GetChild(0);
        trees = transform.GetChild(1);
    }

    

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if(generateCenter == null)
        {
            generateCenter = transform.GetChild(0);
        }

        Vector3 p0 = generateCenter.position + new Vector3(-width * 0.5f, 0, -height * 0.5f);
        Vector3 p1 = generateCenter.position + new Vector3(width * 0.5f, 0, -height * 0.5f);
        Vector3 p2 = generateCenter.position + new Vector3(width * 0.5f, 0, height * 0.5f);
        Vector3 p3 = generateCenter.position + new Vector3(-width * 0.5f, 0, height * 0.5f);

        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p0);
    }

    private void OnValidate()
    {
        if (forestGenerate) 
        {
            GenerateTrees();            // forestGenerate 가 눌러지면 나무 생성
            forestGenerate = false;
        }

        if (reset)
        {
            ResetTrees();
            reset = false;
        }

        
    }

    void ResetTrees()
    {
        //if (trees == null)
        //    trees = transform.GetChild(1);

        //while (trees.childCount > 0) 
        //{
        //    Transform del = trees.GetChild(0);
        //    del.SetParent(null);
        //    DestroyImmediate(del.gameObject);
        //}

        serializeNumber = 0;
    }

    /// <summary>
    /// 나무 생성 함수
    /// </summary>
    void GenerateTrees()
    {
        // 필요한 트랜스폼 찾기
        if (generateCenter == null)
            generateCenter = transform.GetChild(0);

        if (trees == null)
            trees = transform.GetChild(1);

        // 생성 영역 최소/최대값
        Vector3 min = generateCenter.position + new Vector3(-width * 0.5f, 0, -height * 0.5f);
        Vector3 max = generateCenter.position + new Vector3(width * 0.5f, 0, height * 0.5f);

        // 개수만큼 나무 생성
        for (int i = 0; i < treeCount; i++)
        {
            GameObject tree = Instantiate(treePrefabs[(int)type], trees);
            tree.transform.position = new Vector3(
                Random.Range(min.x, max.x),
                tree.transform.position.y,
                Random.Range(min.z, max.z)
                );
            tree.name = $"{type}_{serializeNumber}";
            serializeNumber++;

            ObjectRandomize objectRandomize = tree.GetComponent<ObjectRandomize>();
            objectRandomize.Randomize();
        }
    }
#endif
}