using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI_Base : MonoBehaviour
{
    // 이 UI가 가지고 있는 슬롯
    InvenSlot slot;

    // 슬롯을 확인하기 위한 프로퍼티
    public InvenSlot InvenSlot => slot;

    public uint Index => slot.Index;

    Image itemIcon;

    TextMeshProUGUI itemCount;

    protected virtual void Awake()
    {
        Transform child = transform.GetChild(0);
        itemIcon = child.GetComponent<Image>();
        child = transform.GetChild(1);
        itemCount = child.GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// 슬롯을 초기화 하는 함수(invenslot 과 invenslotUI를 연결한다.)
    /// </summary>
    /// <param name="slot"></param>
    public virtual void InitializeSlot(InvenSlot slot)
    {
        this.slot = slot;

        // 이벤트 중복 발생을 방지하기 위해 일단 빼고 다시 더해준다.
        this.slot.ClearDelegates();
        this.slot.onSlotItemChange += Refresh;

        Refresh();
    }

    /// <summary>
    /// 슬롯 UI에 표시되는 화면 갱신하는 함수
    /// </summary>
    void Refresh()
    {
        if(InvenSlot.IsEmpty)
        {
            // 슬롯이 비어있다.
            itemIcon.color = Color.clear;
            itemIcon.sprite = null;
            itemCount.text = string.Empty;
        }
        else
        {
            // 슬롯에 아이템이 있다.
            itemIcon.sprite = InvenSlot.ItemData.itemIcon;
            itemIcon.color = Color.white;
            itemCount.text = InvenSlot.ItemCount.ToString();
        }

        OnRefresh();
    }

    /// <summary>
    /// 화면 갱신 타이밍에 자식 클래스에서 개별적으로 실행할 코드용 함수
    /// </summary>
    protected virtual void OnRefresh()
    {
        // InvenSlotUI 에서 장비 여부 표시 갱신용
    }
}
