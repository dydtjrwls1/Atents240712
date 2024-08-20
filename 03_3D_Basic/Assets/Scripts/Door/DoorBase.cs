using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 모든 문의 부모 클래스
public class DoorBase : MonoBehaviour
{
    Animator animator;

    readonly int IsOpen_Hash = Animator.StringToHash("IsOpen");

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // 문이 열리고 닫힐 때 각 종류별로 따로 처리해야 하는 일을 기록할 가상함수(빈 함수)
    protected virtual void OnOpen() { }
    protected virtual void OnClose() { }

    // 문을 여는 함수
    public void Open()
    {
        animator.SetBool(IsOpen_Hash, true);
        OnOpen();
    }

    // 문을 닫는 함수
    public void Close()
    {
        animator.SetBool(IsOpen_Hash, false);
        OnClose();
    }
}
