using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    Player player;

    // 씬에 있는 플레이어에 접근하기 위한 프로퍼티(읽기전용)
    public Player Player
    {
        get 
        {
            if (player == null)
                OnInitialize(); // OnInitialize 전에 호출하면 일단 초기화
            return player;
        }
    }
    protected override void OnInitialize()
    {
       player = FindAnyObjectByType<Player>();
    }
}
