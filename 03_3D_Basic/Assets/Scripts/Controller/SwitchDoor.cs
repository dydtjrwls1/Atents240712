using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDoor : MonoBehaviour, IInteractable
{
    // on 상태, off 상태
    // on 상태가 되엇을 때 연결된 문이 열린다. 
    // off 상태가 되었을 때 문이 닫힌다.
    // 연결되는 문은 수동문만 가능.
    // on/off 가 번갈아가면서 설정된다.

    // 스위치 사용 쿨타임 최소값 0.5초, 재생 애니메이션 시간이 0.5초 이기 때문.
    [Min(0.5f)]
    public float coolDown = 0.5f;

    // 남은 쿨타임이 0이하면 사용 가능
    public bool CanUse => remainsCoolDown < 0.0f;

    // 이 스위치가 열 수 있는 문
    public DoorManualBase targetDoor;

    // 현재 남아있는 쿨타임
    float remainsCoolDown = 0.0f;

    bool isOn = false;

    protected virtual bool IsOn
    {
        get => isOn;
        set
        {
            isOn = value;
            animator.SetBool(SwitchOn_Hash, IsOn);
            if (targetDoor != null)
            {
                targetDoor.Use();
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