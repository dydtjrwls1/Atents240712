using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test05_Slime : TestBase
{
    public SpriteRenderer[] spriteRenderers;

    Material[] materials;

    public bool isOutLineChange;
    public bool isInnerLineChange;

    // Line 관련 프로퍼티 설정값 (최소 두께, 최대 두께)
    [Range(0f, 0.04f)]
    public float lineMinThickness = 0.0f;
    [Range(0f, 0.04f)]
    public float lineMaxThickness = 0.01f;

    [Space]
    public bool isPhaseChange;
    public bool isPhaseReverseChange;
    public bool isDissolveChange;

    // 변화 속도
    [Space]
    [Range(0.01f, 5.0f)]
    public float frequency = 5.0f;

    // 누적 시간
    float[] elapsedTimes;

    // 라인 두께 최소값 프로퍼티
    public float LineMinThickness 
    {
        get => lineMinThickness;
        set
        {
            materials[0].SetFloat(Thickness_Min_Hash, value);
            materials[1].SetFloat(Thickness_Min_Hash, value);
            lineMaxThickness = value;
        }
    }

    // 라인 두께 최대값 프로퍼티
    public float LineMaxThickness
    {
        get => lineMaxThickness;
        set
        {
            materials[0].SetFloat(Thickness_Max_Hash, value);
            materials[1].SetFloat(Thickness_Max_Hash, value);
            lineMinThickness = value;
        }
    }

    // 해쉬값
    readonly int Thickness_Hash = Shader.PropertyToID("_Thickness");
    readonly int Thickness_Min_Hash = Shader.PropertyToID("_Thickness_Min");
    readonly int Thickness_Max_Hash = Shader.PropertyToID("_Thickness_Max");
    readonly int Fade_Hash = Shader.PropertyToID("_Fade");
    readonly int Split_Hash = Shader.PropertyToID("_Split");

    private void Start()
    {
        materials = new Material[spriteRenderers.Length];

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            materials[i] = spriteRenderers[i].material;
        }

        // 누적 시간 배열 초기화
        elapsedTimes = new float[spriteRenderers.Length];

        // 시작 설정
        materials[0].SetFloat(Thickness_Hash, 0.0f);
        materials[1].SetFloat(Thickness_Hash, 0.0f);
        materials[2].SetFloat(Split_Hash, 0.0f);
        materials[3].SetFloat(Split_Hash, 0.0f);
        materials[4].SetFloat(Fade_Hash, 0.0f);
    }

    private void Update()
    {
        if (isOutLineChange)
        {
            float thickness = LineMinThickness + (LineMaxThickness - LineMinThickness) * GetRatio(ref elapsedTimes[0]); ;
            materials[0].SetFloat(Thickness_Hash, thickness);
        }

        if (isInnerLineChange)
        {
            float thickness = LineMinThickness + (LineMaxThickness - LineMinThickness) * GetRatio(ref elapsedTimes[1]);
            materials[1].SetFloat(Thickness_Hash, thickness);
        }

        if (isPhaseChange)
        {
            materials[2].SetFloat(Split_Hash, GetRatio(ref elapsedTimes[2]));
        }

        if (isPhaseReverseChange)
        {
            materials[3].SetFloat(Split_Hash, GetRatio(ref elapsedTimes[3]));
        }

        if (isDissolveChange)
        {
            materials[4].SetFloat(Fade_Hash, GetRatio(ref elapsedTimes[4]));
        }
    }

    /// <summary>
    /// 각 누적시간 별 시간진행에 따른 비율 반환하는 함수
    /// </summary>
    /// <param name="elapsedTime">누적 시간</param>
    /// <returns>0~1 으로 변환한 값</returns>
    float GetRatio(ref float elapsedTime)
    {
        elapsedTime += Time.deltaTime * frequency; // ref로 받았기 때문에 수정 가능
        return (Mathf.Cos(elapsedTime) + 1.0f) * 0.5f; 
    }
}