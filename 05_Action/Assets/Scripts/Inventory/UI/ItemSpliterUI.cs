using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemSpliterUI : MonoBehaviour
{
    // 임시 슬롯이 비어있는 상태에서 일반 슬롯을 쉬프트를 누른채로 클릭하면 열린다.
    // 기본적으로 나누는 개수가 1로 시작한다
    // 나눌 수 있는 최소치는 1이고 최대치는 쉬프트 클릭한 슬롯에 들어있는 아이템의 개수 -1 이다. (인풋필드, 버튼, 슬라이더 3개 다 적용되어야 함)
    // 열릴 때 쉬프트 클릭한 슬롯의 아이콘이 보여야한다.
    // OK 버튼을 누르면 나눈 개수만큼 원본슬롯에서 덜어서 임시 슬롯으로 보내고 닫힌다.
    // Cancel 버튼을 누르면 그냥 닫힌다.
    PlayerInputActions inputActions;

    Image m_Icon;

    TMP_InputField m_InputField;

    Slider m_Slider;

    // 아이템을 나눌 슬롯
    InvenSlot targetSlot;

    // 아이템을 나눌 최소 개수
    const uint MinItemCount = 1;

    // 아이템을 나눌 최대 개수를 설정하는 프로퍼티
    uint MaxItemCount => targetSlot != null ? targetSlot.ItemCount - 1 : MinItemCount;

    // 나눌 개수
    uint count = MinItemCount;

    // 아이템을 나눌 개수를 설정하는 프로퍼티
    uint Count
    {
        get => count;
        set
        {
            count = Math.Clamp(value, MinItemCount, MaxItemCount);
            // 인풋 필드
            m_InputField.text = count.ToString();
            // 슬라이더
            m_Slider.value = count;
        }
    }

    // ok 버튼 눌렸음을 알리는 델리게이트 (uint : 슬롯의 인덱스 , uint : 나눌 개수)
    public event Action<uint, uint> onOkClick = null;

    // Cancel 버튼이 눌렸음을 알리는 델리게이트
    public event Action onCancelClick = null;

    private void Awake()
    {
        inputActions = new PlayerInputActions();

        Transform child = transform.GetChild(0);    
        m_Icon = child.GetComponent<Image>();

        child = transform.GetChild(1);
        m_InputField = child.GetComponent<TMP_InputField>();
        m_InputField.onValueChanged.AddListener((text) => 
        {
            if( uint.TryParse(text, out uint result) )
            {
                Count = result; // 변환된 값으로 설정한다.
            }
            else
            {
                Count = MinItemCount; // 음수를 입력했으므로 최소값으로 설정한다.
            }
        });


        child = transform.GetChild(2);
        Button m_PlusButton = child.GetComponent<Button>();
        m_PlusButton.onClick.AddListener(() => { Count++; });

        child = transform.GetChild(3);
        Button m_MinusButton = child.GetComponent<Button>();
        m_MinusButton.onClick.AddListener(() => { Count--; });

        child = transform.GetChild(4);
        m_Slider = child.GetComponent<Slider>();
        m_Slider.minValue = MinItemCount; // 슬라이더의 최소값은 1
        m_Slider.onValueChanged.AddListener((value) => 
        {
            Count = (uint)value;
        });

        child = transform.GetChild(5);
        Button m_OkButton = child.GetComponent<Button>();
        m_OkButton.onClick.AddListener(() =>
        {
            onOkClick?.Invoke(targetSlot.Index, Count);
            Close();
        });

        child = transform.GetChild(6);
        Button m_CancelButton = child.GetComponent<Button>();

        Close();
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.Click.performed += OnClick;
        inputActions.UI.Wheel.performed += OnWheel;
    }

    private void OnClick(InputAction.CallbackContext _)
    {
        // UI 밖을 클릭하면 닫힌다.
        if (!MousePointInRect())
        {
            Close();
        }
    }

    private void OnWheel(InputAction.CallbackContext context)
    {
        // 휠 움직임에 따라 카운트가 증가 감소한다.
        if (MousePointInRect())
        {
            if(context.ReadValue<float>() > 0)
            {
                // 위로 올리기
                Count++;
            }
            else
            {
                // 아래로 내리기
                Count--;
            }
        }
    }

    // 마우스 커서 위치가 UI 안이면 True 밖이면 False
    bool MousePointInRect()
    {
        Vector2 screen = Mouse.current.position.ReadValue();
        Vector2 diff = screen - (Vector2)transform.position;
        
        RectTransform rect = (RectTransform)transform;
        return rect.rect.Contains(diff); // diff 가 피봇 기준으로 떨어져 있는지 아닌지 확인하는 함수
    }

    // 아이템 분리창을 여는 함수 (true 면 열었다, false 면 닫았다)
    public bool Open(InvenSlot target)
    {
        bool result = false;
        if(!target.IsEmpty && target.ItemCount > MinItemCount) // target 슬롯에 아이템이 들어있고 개수가 1개 초과일 때만 연다.
        {
            targetSlot = target;
            m_Icon.sprite = targetSlot.ItemData.itemIcon;
            m_Slider.maxValue = MaxItemCount;
            Count = targetSlot.ItemCount / 2;

            result = true;
            gameObject.SetActive(true);
        }

        return result;
    }

    void Close()
    {
        gameObject.SetActive(false);
    }
}
