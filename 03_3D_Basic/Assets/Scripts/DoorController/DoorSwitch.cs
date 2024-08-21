using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour, IInteractable
{
    // on 상태, off 상태
    // on 상태가 되엇을 때 연결된 문이 열린다. 
    // off 상태가 되었을 때 문이 닫힌다.
    // 연결되는 문은 수동문만 가능.
    // on/off 가 번갈아가면서 설정된다.
    public DoorManual door;

    public float coolDown = 1.0f;

    Animator animator;

    float remainsCoolDown = 0.0f;

    bool isOn;

    readonly int SwitchOn_Hash = Animator.StringToHash("SwitchOn");

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        remainsCoolDown -= Time.deltaTime;
    }

    public bool CanUse => remainsCoolDown < 0.0f;

    public void Use()
    {
        if (CanUse)
        {
            if (isOn)
            {
                isOn = false;
                animator.SetBool(SwitchOn_Hash, isOn);
            }
            else
            {
                isOn = true;
                animator.SetBool(SwitchOn_Hash, isOn);
            }

            door.Use();
            remainsCoolDown = coolDown;
        }
    }
}