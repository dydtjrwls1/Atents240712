#define PrintTestLog
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvenSlotUI : SlotUI_Base, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler

{
    TextMeshProUGUI equipText;

    // 드래그의 시작을 알리는 델리게이트(uint : 드래그 시작한 슬롯의 인덱스)
    public event Action<uint> onDragBegin = null;

    // 드래그의 끝을 알리는 델리게이트(uint : 드랍을 성공했으면 인덱스를 보내고 실패했으면 null 을 보낸다.)
    public event Action<uint?> onDragEnd = null;

    public event Action<ItemData> onPointerEnter = null;

    public event Action<bool> onPointerExit = null;

    public event Action<Vector2> onPointerMove = null;

    public event Action onPointerUp = null;

    public event Action<bool> onPointerDown = null;

    public event Action<uint> onPointerClick = null;

    protected override void Awake()
    {
        base.Awake();
        Transform child = transform.GetChild(2);
        equipText = child.GetComponent<TextMeshProUGUI>();
    }

    public override void InitializeSlot(InvenSlot slot)
    {
        ClearDelegates();
        base.InitializeSlot(slot);
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

    public void OnBeginDrag(PointerEventData eventData)
    {
#if PrintTestLog
        Debug.Log($"드래그 시작. [{Index}] 번 슬롯");
#endif
        onDragBegin?.Invoke(Index);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Begin 과 End 를 발동시키기 위해 필요
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject; // 드래그 끝나는 지점에 있는 게임오브젝트
        if (obj != null)
        {
            // 마우스 위치에 무언가 있다.
            uint? endIndex = null;
            InvenSlotUI endSlot = obj.GetComponent<InvenSlotUI>();

            if(endSlot != null)
            {
                // 슬롯이다.
                endIndex = endSlot.Index;
#if PrintTestLog
                Debug.Log($"드래그 종료 : [{endIndex}]번 슬롯.");
#endif
            }
            else
            {
                // 슬롯이 아니다.
#if PrintTestLog
                Debug.Log($"슬롯이 아닙니다. [{obj.name}]");
#endif
            }
            onDragEnd?.Invoke(endIndex);
        }
        else
        {
            // 마우스 위치에 그 어떤 게임오브젝트도 없다.
#if PrintTestLog
            Debug.Log("어떤 UI도 없다.");
#endif
        }
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        onPointerMove?.Invoke(eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onPointerExit?.Invoke(InvenSlot.IsEmpty);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onPointerEnter?.Invoke(InvenSlot.ItemData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onPointerDown?.Invoke(InvenSlot.IsEmpty);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onPointerUp?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onPointerClick?.Invoke(InvenSlot.Index);
    }

    public void ClearDelegates()
    {
        onPointerClick = null;
        onPointerDown = null;
        onPointerUp = null;
        onPointerExit = null;
        onPointerEnter = null;
        onPointerMove = null;
        onDragBegin = null;
        onDragEnd = null;
        
    }
}
