using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpliterUI : MonoBehaviour
{
    // 임시 슬롯이 비어있는 상태에서 일반 슬롯을 쉬프트를 누른채로 클릭하면 열린다.
    // 기본적으로 나누는 개수가 1로 시작한다
    // 나눌 수 있는 최소치는 1이고 최대치는 쉬프트 클릭한 슬롯에 들어있는 아이템의 개수 -1 이다. (인풋필드, 버튼, 슬라이더 3개 다 적용되어야 함)
    // 열릴 때 쉬프트 클릭한 슬롯의 아이콘이 보여야한다.
    // OK 버튼을 누르면 나눈 개수만큼 원본슬롯에서 덜어서 임시 슬롯으로 보내고 닫힌다.
    // Cancel 버튼을 누르면 그냥 닫힌다.
    CanvasGroup m_CanvasGroup;

    Image m_Icon;

    TMP_InputField m_InputField;

    Button m_PlusButton;
    Button m_MinusButton;

    Button m_OKButton;
    Button m_CancelButton;

    Slider m_Slider;

    uint m_SplitedSlotIndex;

    uint m_MaxCount;

    uint m_CurrentCount;
    uint CurrentCount
    {
        get => m_CurrentCount;
        set
        {
            if (m_CurrentCount != value)
            {
                m_CurrentCount = value;
                m_CurrentCount = (uint)Mathf.Clamp(m_CurrentCount, 1, m_MaxCount);
                m_Slider.value = m_CurrentCount;
                m_InputField.text = m_CurrentCount.ToString();
            }
        }
    }

    public Action<uint, uint> onOKButtonClicked = null;

    private void Awake()
    {
        m_CanvasGroup = GetComponent<CanvasGroup>();

        Transform child = transform.GetChild(0);    
        m_Icon = child.GetComponent<Image>();

        child = transform.GetChild(1);
        m_InputField = child.GetComponent<TMP_InputField>();

        child = transform.GetChild(2);
        m_PlusButton = child.GetComponent<Button>();

        child = transform.GetChild(3);
        m_MinusButton = child.GetComponent<Button>();

        child = transform.GetChild(4);
        m_Slider = child.GetComponent<Slider>();

        child = transform.GetChild(5);
        m_OKButton = child.GetComponent<Button>();

        child = transform.GetChild(6);
        m_CancelButton = child.GetComponent<Button>();
    }

    private void Start()
    {
        DeactivateCanvas();

        m_PlusButton.onClick.AddListener(PlusButtonClicked);
        m_MinusButton.onClick.AddListener(MinusButtonClicekd);
        m_OKButton.onClick.AddListener(OKButtonClicked);
        m_CancelButton.onClick.AddListener(CancelButtonClicked);

        m_Slider.onValueChanged.AddListener(SliderValueChanged);
    }

    public void Open(InvenSlot slot, uint itemCount)
    {
        ActivateCanvas();

        m_MaxCount = itemCount - 1;
        m_SplitedSlotIndex = slot.Index;

        m_Icon.sprite = slot.ItemData.itemIcon;
        m_Slider.maxValue = m_MaxCount;
        CurrentCount = 1;
    }

    private void PlusButtonClicked()
    {
        CurrentCount++;
    }

    private void MinusButtonClicekd()
    {
        CurrentCount--;
    }

    private void SliderValueChanged(float value)
    {
        CurrentCount = (uint)value;
    }

    private void OKButtonClicked()
    {
        DeactivateCanvas();

        onOKButtonClicked?.Invoke(m_SplitedSlotIndex, CurrentCount);
    }

    private void CancelButtonClicked()
    {
        DeactivateCanvas();
    }

    private void ActivateCanvas()
    {
        m_CanvasGroup.alpha = 1.0f;
        m_CanvasGroup.blocksRaycasts = true;
        m_CanvasGroup.interactable = true;
    }

    private void DeactivateCanvas()
    {
        m_CanvasGroup.alpha = 0.0f;
        m_CanvasGroup.blocksRaycasts = false;
        m_CanvasGroup.interactable = false;
    }
}
