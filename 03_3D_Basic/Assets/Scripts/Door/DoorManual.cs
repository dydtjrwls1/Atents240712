using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorManual : DoorBase, IInteractable
{
    // 재사용 쿨타임
    public float coolDown = 1.0f;

    // 남아 있는 쿨타임
    float remainsCoolDown = 0.0f;

    // 근처에 플레이어가 접근하면 상호작용용 단축키를 알려주는 글자
    TextMeshPro text;

    // 문이 열려있는지를 표시하는 변수(true면 열려있다, false 면 닫혀있다)
    bool isOpen;

    // 현재 이 오브젝트를 사용가능한지 판단하기 위한 프로퍼티( 인터페이스에 있는 프로퍼티 구현 )
    public bool CanUse => remainsCoolDown < 0.0f;

    protected override void Awake()
    {
        base.Awake();
        text = GetComponentInChildren<TextMeshPro>(true);
    }

    void Update()
    {
        remainsCoolDown -= Time.deltaTime;
    }

    protected virtual void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            ShowInfoPanel(true);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowInfoPanel(false);
        }
    }
    /// <summary>
    /// 문을 사용하는 함수 (인터페이스 구현)
    /// </summary>
    public void Use()
    {
        if (CanUse)
        {
            if (isOpen)
            {
                Close();
            } else
            {
                Open();
            }

            remainsCoolDown = coolDown;
        }
    }

    /// <summary>
    /// 문이 열렸음을 표시하는 기능
    /// </summary>
    protected override void OnOpen()
    {
        isOpen = true;
    }

    /// <summary>
    /// 문이 닫혔음을 표시하는 기능
    /// </summary>
    protected override void OnClose()
    {
        isOpen = false;
    }

    void ShowInfoPanel(bool isShow = true)
    {
        if (isShow)
        {
            Vector3 cameraToDoor = transform.position - Camera.main.transform.position;

            float angle = Vector3.Angle(transform.forward, cameraToDoor);   
            if(angle > 90)
            {
                // 카메라가 문 쪽에 있다.
                text.transform.rotation = transform.rotation * Quaternion.Euler(0, 180, 0);
            }
            else
            {
                // 카메라가 문 뒤쪽에 있다.
                text.transform.rotation = transform.rotation; // 인포의 회전을 문의 회전과 일치시킨다.
            }
        }
        else
        {

        }

        text.gameObject.SetActive(isShow);
    }
}
