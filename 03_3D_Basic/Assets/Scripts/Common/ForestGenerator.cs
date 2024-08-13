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

    bool forestGenerate = false;

    public float width = 10.0f;
    public float height = 10.0f;
    Transform generateCenter;

    public int treeCount = 10;
}