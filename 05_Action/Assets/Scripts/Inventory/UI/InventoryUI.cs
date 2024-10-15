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

    DetailInfoUI detailInfoUI;

    ItemSpliterUI itemSpliterUI;

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

        child = transform.GetChild(3);
        detailInfoUI = child.GetComponent<DetailInfoUI>();

        tempSlotUI = GetComponentInChildren<InvenTempSlotUI>();

        itemSpliterUI = GetComponentInChildren<ItemSpliterUI>();
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
            slotsUIs[i].onPointerEnter += detailInfoUI.OnItemDetailInfoOpen;
            slotsUIs[i].onPointerExit += detailInfoUI.OnItemDetailInfoClose;
            slotsUIs[i].onPointerMove += detailInfoUI.MovePosition;
            slotsUIs[i].onPointerUp += detailInfoUI.OnItemDetailInfoUp;
            slotsUIs[i].onPointerDown += detailInfoUI.OnItemDetailInfoDown;
            slotsUIs[i].onPointerClick += OnSlotClick;
        }
        tempSlotUI.InitializeSlot(inven.TempSlot);

        //Close();
    }

    private void OnSlotClick(uint? index)
    {
        if (!tempSlotUI.InvenSlot.IsEmpty)
        {
            OnItemMoveEnd(index);
        }
        else
        {
            uint itemCount = slotsUIs[index.Value].InvenSlot.ItemCount;

            // 쉬프트 버튼을 누르고 해당 UI 슬롯에 들어있는 아이템의 개수가 1 이상이면
            if (itemCount > 1)
            {
                itemSpliterUI.Open(slotsUIs[index.Value].InvenSlot, itemCount);
            }
        }
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
            detailInfoUI.OnItemDetailInfoOpen(inven[index.Value].ItemData);
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
