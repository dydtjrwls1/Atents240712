using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : RecycleObject
{
    ItemData data = null;

    SpriteRenderer spriteRenderer = null;

    public ItemData Data
    {
        get => data;
        set
        {
            if(data == null)
            {
                data = value;
                spriteRenderer.sprite = data.itemIcon;
            }
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }

    protected override void OnDisable()
    {
        data = null;
        base.OnDisable();
    }

    // 아이템을 획득해서 비활성화 되는 함수
    public void CollectedItem()
    {
        gameObject.SetActive(false);
    }
}
