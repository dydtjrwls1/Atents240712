using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    // 플레이어
    Player player;

    // 플레이어 확인용 프로퍼티
    public Player Player => player;

    VirtualStick stick;

    public VirtualStick Stick => stick;

    VirtualButton virtualButton;

    public VirtualButton VirtualButton => virtualButton;

    /// <summary>
    /// 초기화용 함수
    /// </summary>
    protected override void OnInitialize()
    {
        player = FindAnyObjectByType<Player>();
        stick = FindAnyObjectByType<VirtualStick>();
        virtualButton = FindAnyObjectByType<VirtualButton>();
    }
}
