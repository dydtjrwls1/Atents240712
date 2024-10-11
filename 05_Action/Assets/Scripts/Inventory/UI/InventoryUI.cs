using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(CanvasGroup))]
public class InventoryUI : MonoBehaviour
{
    // 이 UI가 보여줄 인벤토리
    Inventory inven;

    // 인벤토리에 들어있는 slot들의 UI
    InvenSlotUI[] slotsUIs;

    // 임시 슬롯의 UI
    InvenTempSlotUI tempSlotUI;

    // 입력 처리용
    PlayerInputActions inputActions;

    CanvasGroup canvasGroup;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        canvasGroup = GetComponent<CanvasGroup>();

        Transform child = transform.GetChild(0);
        slotsUIs = child.GetComponentsInChildren<InvenSlotUI>();

        child = transform.GetChild(1);
        Button close = child.GetComponent<Button>();
        close.onClick.AddListener(Close);

        tempSlotUI = GetComponentInChildren<InvenTempSlotUI>();
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.InvenOnOff.performed += OnInvenOnOff;
        inputActions.UI.Click.canceled += OnItemDrop;
    }

    private void OnDisable()
    {
        inputActions.UI.Click.canceled -= OnItemDrop;
        inputActions.UI.InvenOnOff.performed -= OnInvenOnOff;
        inputActions.UI.Disable();
    }

    public void InitializeInventory(Inventory inventory)
    {
        inven = inventory;
        for(uint i = 0;  i < slotsUIs.Length; i++)
        {
            slotsUIs[i].InitializeSlot(inven[i]);
            slotsUIs[i].onDragBegin += OnItemMoveBegin;
            slotsUIs[i].onDragEnd += OnItemMoveEnd;
        }
        tempSlotUI.InitializeSlot(inven.TempSlot);

        //Close();
    }

    // 슬롯에서 드래그가 시작되었을 때 실행되는 함수
    private void OnItemMoveBegin(uint index)
    {
        inven.MoveItem(index, tempSlotUI.Index);
    }

    // 드래그가 끝났을 때 실행되는 함수
    private void OnItemMoveEnd(uint? index)
    {
        if (index.HasValue)
        {
            inven.MoveItem(tempSlotUI.Index, index.Value);
        }
        else
        {

        }
    }

    private void OnItemDrop(InputAction.CallbackContext _)
    {
    }

    private void OnInvenOnOff(InputAction.CallbackContext _)
    {
        if (canvasGroup.interactable)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    void Open()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    void Close()
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }


}
