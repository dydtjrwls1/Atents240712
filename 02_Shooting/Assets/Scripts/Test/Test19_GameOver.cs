using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test19_GameOver : TestBase
{
    public int score = 100;
    Player player;
    ScoreText scoreText;

    private void Start()
    {
        player = GameManager.Instance.Player;
        scoreText = GameManager.Instance.ScoreTextUI;
    }

    // 테스트용 코드
#if UNITY_EDITOR
    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        player.TestDeath();
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        scoreText.AddScore(score);
    }
#endif
}
