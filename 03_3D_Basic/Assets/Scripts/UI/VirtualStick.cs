using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class VirtualStick : MonoBehaviour, IDragHandler, IEndDragHandler
{
    // 핸들의 rect
    RectTransform handle;

    // 핸들이 움직일 영역의 rect
    RectTransform background;

    float stickRange;

    public Action<Vector2> onMoveInput = null;

    private void Awake()
    {
        background = transform as RectTransform;

        Transform child = transform.GetChild(0);
        handle = child as RectTransform;

        stickRange = (background.rect.width - handle.rect.width) * 0.5f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background,                         // 이 rect 영역의 원점 기준으로
            eventData.position,                 // 이 스크린 좌표가
            eventData.pressEventCamera,         // 이 카메라 기준으로
            out Vector2 localMove);             // 이만큼 움직였다 (로컬 기준)

        localMove = Vector2.ClampMagnitude(localMove, stickRange);

        InputUpdate(localMove);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        handle.anchoredPosition = Vector2.zero;

        onMoveInput?.Invoke(Vector2.zero);
    }

    void InputUpdate(Vector2 inputDelta)
    {
        // 움직임 처리
        handle.anchoredPosition = inputDelta;

        onMoveInput?.Invoke(inputDelta / stickRange); // 크기를 0 ~ 1 사이로 정규화해서 보냄
    }

    public void Disconnect()
    {
        onMoveInput = null;
    }
}
