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

    MoneyPanelUI moneyPanelUI;

    DetailInfoUI detailInfoUI;

    ItemSpliterUI itemSpliterUI;

    SortPanelUI sortPanelUI;

    // 입력 처리용
    PlayerInputActions inputActions;

    CanvasGroup canvasGroup;

    public Player Owner => inven.Owner;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        canvasGroup = GetComponent<CanvasGroup>();

        Transform child = transform.GetChild(0);
        slotsUIs = child.GetComponentsInChildren<InvenSlotUI>();

        child = transform.GetChild(1);
        Button close = child.GetComponent<Button>();
        close.onClick.AddListener(Close);

        child = transform.GetChild(2);
        sortPanelUI = child.GetComponent<SortPanelUI>();

        child = transform.GetChild(3);
        moneyPanelUI = child.GetComponent<MoneyPanelUI>();

        child = transform.GetChild(4);
        detailInfoUI = child.GetComponent<DetailInfoUI>();

        child = transform.GetChild(5);
        itemSpliterUI = child.GetComponent<ItemSpliterUI>();

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
            slotsUIs[i].onPointerEnter += detailInfoUI.OnItemDetailInfoOpen;
            slotsUIs[i].onPointerExit += detailInfoUI.OnItemDetailInfoClose;
            slotsUIs[i].onPointerMove += detailInfoUI.MovePosition;
            slotsUIs[i].onPointerUp += detailInfoUI.ShowItemDetailInfo;
            slotsUIs[i].onPointerDown += detailInfoUI.OnItemDetailInfoDown;
            slotsUIs[i].onPointerClick += OnSlotClick;
        }

        tempSlotUI.InitializeSlot(inven.TempSlot);

        itemSpliterUI.onOkClick += OnSpliterOK;
        itemSpliterUI.onCancelClick += OnSpliterCancel;

        Owner.PlayerInventory.onMoneyChange += moneyPanelUI.Refresh;
        moneyPanelUI.Refresh(Owner.PlayerInventory.Money);

        sortPanelUI.onSortRequest += (sort) =>
        {
            bool isAcsending = true;
            if (sort == ItemSortCriteria.Price)
            {
                isAcsending = false;
            }

            inven.MergeItems();
            inven.SlotSorting(sort, isAcsending);
        };

        Close();
    }

    

    private void OnSlotClick(uint index)
    {
        if (tempSlotUI.InvenSlot.IsEmpty)
        {
            bool isShiftPress = Keyboard.current.shiftKey.ReadValue() > 0; // 쉬프트 키를 눌렀습니까?

            if (isShiftPress)
            {
                ItemSpliterOpen(index);
            }
            else
            {
                // 쉬프트를 누르지 않았다면 아이템 사용이 목적
                InvenSlot slot = inven[index];
                inven[index].UseItem(Owner.gameObject);
                inven[index].EquipItem(Owner.gameObject);
            }
        }
        else
        {
            OnItemMoveEnd(index);
        }
        
    }

    private void OnSpliterCancel()
    {
        throw new NotImplementedException();
    }

    private void OnSpliterOK(uint index, uint count)
    {
        inven.SplitItem(index, count);
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

            if (tempSlotUI.InvenSlot.IsEmpty)
            {
                detailInfoUI.OnItemDetailInfoOpen(inven[index.Value].ItemData);
            }
        }
    }

    private void OnItemDrop(InputAction.CallbackContext _)
    {
        Vector2 screen = Mouse.current.position.ReadValue();
        Vector2 diff = screen - (Vector2)transform.position;

        RectTransform rectTransform = (RectTransform)transform;
        if (!rectTransform.rect.Contains(diff)) // 인벤토리 영역 밖이라면
        {
            // 아이템 드랍
            tempSlotUI.ItemDrop(screen); // 임시 슬롯에 있는 아이템을 버린다
        }
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

    void ItemSpliterOpen(uint index)
    {
        InvenSlotUI target = slotsUIs[index];
        itemSpliterUI.transform.position = target.transform.position + Vector3.up * 100;
        if (itemSpliterUI.Open(target.InvenSlot))
        {
            detailInfoUI.HideItemDetailInfo();
        }
    }
#if UNITY_EDITOR
    public void Test_Open()
    {
        Open();
    }
#endif
}
