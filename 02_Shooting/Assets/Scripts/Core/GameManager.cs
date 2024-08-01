using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : SingleTon<GameManager>
{
    Player player;

    // 점수 표시용 UI
    ScoreText scoreTextUI;

    // 씬에 있는 플레이어에 접근하기 위한 프로퍼티(읽기전용)
    public Player Player
    {
        get 
        {
            if (player == null)
                player = FindAnyObjectByType<Player>(); // OnInitialize 전에 호출하면 일단 초기화
            return player;
        }
    }

    public ScoreText ScoreTextUI
    {
        get
        {
            if (scoreTextUI == null)
                scoreTextUI = FindAnyObjectByType<ScoreText>();
            return scoreTextUI;
        }
    }

    protected override void OnInitialize()
    {
       player = FindAnyObjectByType<Player>();

       scoreTextUI = FindAnyObjectByType<ScoreText>();
    }
}
