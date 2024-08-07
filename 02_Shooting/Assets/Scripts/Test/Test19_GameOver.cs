using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test19_GameOver : TestBase
{
    public bool isStartDie = false;
    public int score = 100;
    Player player;
    ScoreText scoreText;

#if UNITY_EDITOR
    private void Start()
    {
        player = GameManager.Instance.Player;
        scoreText = GameManager.Instance.ScoreTextUI;

        if (isStartDie)
        {
            player.TestDeath();
        }
    }

    // 테스트용 코드
    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        player.TestDeath();
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        scoreText.AddScore(score);
    }

    protected override void Test3_performed(InputAction.CallbackContext context)
    {
        RankLine rank = FindFirstObjectByType<RankLine>();
        rank.SetData("기기기", score);
    }

    protected override void Test4_performed(InputAction.CallbackContext context)
    {
        RankPanel panel = FindAnyObjectByType<RankPanel>();
        panel.Test_DefaultRankPanel();
    }

    protected override void Test5_performed(InputAction.CallbackContext context)
    {
        RankPanel panel = FindAnyObjectByType<RankPanel>();
        panel.Test_UpdateRankPanel(score);
    }

    public void Test_OnValueChange()
    {
        Debug.Log("Test_OnValueChange");
    }

    public void Test_EndEdit()
    {
        Debug.Log("Test_EndEdit");
    }

    public void Test_Selected()
    {
        Debug.Log("Test_Selected");
    }

    public void Test_Deselected()
    {
        Debug.Log("Test_Deselected");
    }
#endif
}
