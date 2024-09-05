using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualButton : MonoBehaviour, IPointerClickHandler
{
    public Action onClick = null;

    Image jumpCoolImage;

    private void Awake()
    {
        Transform child = transform.GetChild(1);
        jumpCoolImage = child.GetComponent<Image>();
    }

    private void Start()
    {
        Player player = GameManager.Instance.Player;

        player.onJumpCoolDownChange += (remainsCoolDown) => 
        {
            RefreshCoolTime(remainsCoolDown);
        };
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke();
    }

    // 쿨타임 표시 갱신하는 함수
    public void RefreshCoolTime(float ratio)
    {
        jumpCoolImage.fillAmount = ratio;
    }
}
