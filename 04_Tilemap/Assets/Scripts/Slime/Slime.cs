using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : RecycleObject
{
    SpriteRenderer spriteRenderer;
    Material material;

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
        StartCoroutine(Phase());
    }

    IEnumerator Phase()
    {
        float phaseTime = 1.0f;
        while(phaseTime > 0.0f)
        {
            phaseTime -= Time.deltaTime;
            material.SetFloat(PhaseSplit_Hash, phaseTime);
            yield return null;
        }

        material.SetFloat(PhaseThickness_Hash, 0.0f);
    }

    /// <summary>
    /// Outline을 보여줄지 말지 결정하는 함수
    /// </summary>
    /// <param name="isShow">true => 보여줌 / false => 안 보여줌</param>
    public void ShowOutline(bool isShow = true)
    {
        // Outline
    }

    public void Die()
    {
        // Dissolve
    }
}
