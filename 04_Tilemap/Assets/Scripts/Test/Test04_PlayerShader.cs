using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test04_PlayerShader : TestBase
{
    public SpriteRenderer[] spriteRenderers;

    [ColorUsage(false, true)]
    public Color color;

    Material[] materials;

    readonly int EmissionColor_Hash = Shader.PropertyToID("_EmissionColor");

    private void Start()
    {
        materials = new Material[spriteRenderers.Length]; 

        for(int i = 0; i < spriteRenderers.Length; i++)
        {
            materials[i] = spriteRenderers[i].material;
        }
    }

    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        materials[0].SetColor(EmissionColor_Hash, color);
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        materials[1].SetColor(EmissionColor_Hash, color);
    }

    protected override void Test3_performed(InputAction.CallbackContext context)
    {
        materials[2].SetColor(EmissionColor_Hash, color);
    }
}
