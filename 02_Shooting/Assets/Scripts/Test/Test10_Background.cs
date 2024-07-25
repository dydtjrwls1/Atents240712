using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test10_Background : TestBase
{
    public SpriteRenderer sr;

    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        Debug.Log(sr.sprite.rect);
        Debug.Log(sr.sprite.rect.width);
        Debug.Log(sr.sprite.rect.height);
        Debug.Log(sr.bounds.size.x);

        Debug.Log(sr.bounds.size.y);

        

    }
}
