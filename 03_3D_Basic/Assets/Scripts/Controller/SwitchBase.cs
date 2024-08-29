using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchBase : MonoBehaviour, IInteractable
{
    [Min(0.5f)]
    public float coolDown = 0.5f;

    // 남은 쿨타임이 0이하면 사용 가능
    public bool CanUse => remainsCoolDown < 0.0f;

    // 이 스위치가 열 수 있는 문
    public GameObject targetObject;

    // 현재 남아있는 쿨타임
    float remainsCoolDown = 0.0f;

    // targetObject가 가지는 IIteractable 컴포넌트
    IInteractable target;

    bool isOn = false;

    protected virtual bool IsOn
    {
        get => isOn;
        set
        {
            isOn = value;
            animator.SetBool(SwitchOn_Hash, IsOn);
            if (targetObject != null)
            {
                target.Use();
            }
            else
            {
                Debug.LogWarning("사용할 문이 없습니다.");
            }
        }
    }

    Animator animator;

    readonly int SwitchOn_Hash = Animator.StringToHash("SwitchOn");

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if(targetObject != null)
        {
            target = targetObject.GetComponent<IInteractable>();
        }
        else
        {
            Debug.LogWarning("사용 할 오브젝트가 없습니다.");
        }
        
    }

    void Update()
    {
        remainsCoolDown -= Time.deltaTime;
    }

    public void Use()
    {
        if (CanUse)
        {
            IsOn = !IsOn;
            remainsCoolDown = coolDown;
        }
    }
}
