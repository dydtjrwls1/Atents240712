using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualButton : MonoBehaviour, IPointerClickHandler
{
    public Action onJump = null;

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
            jumpCoolImage.fillAmount = remainsCoolDown;
        };
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        onJump?.Invoke();
    }
}
