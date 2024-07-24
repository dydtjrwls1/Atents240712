using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundStars : Background
{
    SpriteRenderer[] srArray;

    protected override void Awake()
    {
        base.Awake();
        srArray = new SpriteRenderer[bgSlots.Length];
        for(int i = 0; i < bgSlots.Length; i++)
        {
            srArray[i] = bgSlots[i].GetComponent<SpriteRenderer>();
        }
    }

    protected override void MoveRight(int index)
    {
        base.MoveRight(index);
        srArray[index].flipX = !srArray[index].flipX; 
        srArray[index].flipY = !srArray[index].flipY; 
    }
}
