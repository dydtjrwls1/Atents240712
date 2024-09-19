using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : RecycleObject
{
    Material material;

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
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnReset()
    {
        // Phase
        ShowOutline(false);
        material.SetFloat(DissolveFade_Hash, 1.0f);
        material.SetFloat(PhaseThickness_Hash, phaseThickness);
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
        
        gameObject.SetActive(false);
    }
}
