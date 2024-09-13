using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : RecycleObject
{
    SpriteRenderer spriteRenderer;
    Material material;

    public float outlineThickness = 0.005f;

    readonly int OutlineThickness_Hash = Shader.PropertyToID("_OutlineThickness");
    readonly int PhaseSplit_Hash = Shader.PropertyToID("_PhaseSplit");
    readonly int PhaseThickness_Hash = Shader.PropertyToID("_PhaseThickness");
    readonly int DissolveFade_Hash = Shader.PropertyToID("_DissolveFade");

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
    }

    protected override void OnReset()
    {
        // Phase
        material.SetFloat(DissolveFade_Hash, 1.0f);
        material.SetFloat(OutlineThickness_Hash, 0.0f);
        StartCoroutine(Phase());
    }

    IEnumerator Phase()
    {
        float phaseSplit = 1.0f;

        while(phaseSplit > 0.0f)
        {
            phaseSplit -= Time.deltaTime;
            material.SetFloat(PhaseSplit_Hash, phaseSplit);
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
        float dissolveFade = 1.0f;

        while(dissolveFade > 0.0f) 
        {
            dissolveFade -= Time.deltaTime;
            material.SetFloat(DissolveFade_Hash, dissolveFade);
            yield return null;
        }

        material.SetFloat(DissolveFade_Hash, 0.0f);

        gameObject.SetActive(false);
    }
}
