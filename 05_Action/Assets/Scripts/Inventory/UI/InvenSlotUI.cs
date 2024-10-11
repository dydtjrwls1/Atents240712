using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InvenSlotUI : SlotUI_Base
{
    TextMeshProUGUI equipText;

    protected override void Awake()
    {
        base.Awake();
        Transform child = transform.GetChild(2);
        equipText = child.GetComponent<TextMeshProUGUI>();
    }

    protected override void OnRefresh()
    {
        if(InvenSlot.IsEquipped)
        {
            equipText.color = Color.red;
        }
        else
        {
            equipText.color = Color.clear;
        }
    }
}
