using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test05_Slime : TestBase
{
    public SpriteRenderer[] spriteRenderers;

    Material[] materials;

    public bool isOutLineChange;
    public bool isInnerLineChange;
    public bool isPhaseChange;
    public bool isPhaseReverseChange;
    public bool isDissolveChange;

    // 주파수
    [Range(0.01f, 5.0f)]
    public float frequency = 5.0f;

    // Line 관련 프로퍼티 설정값 (최소 두께, 최대 두께)
    [Range(0f, 0.04f)]
    public float lineMinThickness = 0.0f;
    [Range(0f, 0.04f)]
    public float lineMaxThickness = 0.01f;

    // 누적 시간
    float elapsedTime = 0.0f;

    // 현재 진행 정도
    float Current_Delta => (Mathf.Sin(elapsedTime * frequency) + 1) * 0.5f;

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

    // 라인 두께 프로퍼티
    float Thickness => LineMinThickness + (LineMaxThickness - LineMinThickness) * Current_Delta;

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
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (isOutLineChange)
        {
            materials[0].SetFloat(Thickness_Hash, Thickness);
            
        }

        if (isInnerLineChange)
        {
            materials[1].SetFloat(Thickness_Hash, Thickness);
        }

        if (isPhaseChange)
        {
            materials[2].SetFloat(Split_Hash, Current_Delta);
        }

        if (isPhaseReverseChange)
        {
            materials[3].SetFloat(Split_Hash, Current_Delta);
        }

        if (isDissolveChange)
        {
            materials[4].SetFloat(Fade_Hash, Current_Delta);
        }
    }
}