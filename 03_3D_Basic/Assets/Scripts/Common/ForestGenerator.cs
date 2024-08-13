using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestGenerator : MonoBehaviour
{
    // 여러개의 나무 프리펩을 가진다.
    // forestGenerate가 True 가 되면 generateCenter 위치가 중심이고 가로가 width 새로가 height 인 사각형 영역 안에
    // treeCount 만큼의 나무를 Tree1 타입의 나무를 생성한다. (생성된 나무는 Randomize 실행)
    // 생성 영역은 Gizmo로 표시한다.

    public GameObject[] treePrefabs;

    public enum TreeType
    {
        Tree1,
        Tree2
    }

    public TreeType type = TreeType.Tree1;

    public bool forestGenerate = false;

    public float width = 10.0f;
    public float height = 10.0f;
    public Transform generateCenter;

    public int treeCount = 10;

    private void Awake()
    {
        generateCenter = transform.GetChild(0);
    }

    private void OnValidate()
    {
        if (forestGenerate)
        {
            for(int i = 0; i < treeCount; i++)
            {
                GenerateTree();
                forestGenerate = false;
            }
        }
    }

    void GenerateTree()
    {
        float randX = Random.Range(generateCenter.position.x - width * 0.5f, generateCenter.position.x + width * 0.5f);
        float randZ = Random.Range(generateCenter.position.z - height * 0.5f, generateCenter.position.z + height * 0.5f);
        ObjectRandomize tree = Instantiate(treePrefabs[0], new Vector3(randX, 0, randZ), Quaternion.identity).GetComponent<ObjectRandomize>();
        tree.Randomize();
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 p0 = new Vector3(generateCenter.position.x - width * 0.5f, 0, generateCenter.position.z + height * 0.5f);
        Vector3 p1 = new Vector3(generateCenter.position.x + width * 0.5f, 0, generateCenter.position.z + height * 0.5f);
        Vector3 p2 = new Vector3(generateCenter.position.x + width * 0.5f, 0, generateCenter.position.z - height * 0.5f);
        Vector3 p3 = new Vector3(generateCenter.position.x - width * 0.5f, 0, generateCenter.position.z - height * 0.5f);

        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p0);
    }
#endif
}