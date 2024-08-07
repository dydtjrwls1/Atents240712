using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;

public class Test20_SaveLoad : TestBase
{
    public bool isStartDie = false;
    public int score = 100;
    Player player;
    ScoreText scoreText;
    RankPanel panel;

#if UNITY_EDITOR
    private void Start()
    {
        player = GameManager.Instance.Player;
        scoreText = GameManager.Instance.ScoreTextUI;

        panel = FindAnyObjectByType<RankPanel>();

        if (isStartDie)
        {
            player.TestDeath();
        }

        
    }

    // 테스트용 코드
    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        panel.Test_UpdateRankPanel(score);
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        panel.Test_Save();
    }

    protected override void Test3_performed(InputAction.CallbackContext context)
    {
        panel.Test_Load();
    }

    protected override void Test4_performed(InputAction.CallbackContext context)
    {
        scoreText.AddScore(score);
    }

    protected override void Test5_performed(InputAction.CallbackContext context)
    {
        panel.Test_UpdateRankPanel(scoreText.Score);
    }
#endif
}
