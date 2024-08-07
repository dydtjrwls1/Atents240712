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
        // System.IO.Directory
        Debug.Log("저장함");
        // System.IO.File.WriteAllText($"{Application.dataPath}/Save/a.txt", "Hello"); // 폴더가 없어서 죽음
        // Application.dataPath : 에디터에서 실행했을 때는 Assets 폴더를 의미, 빌드해서 실행 했을 때는 "실행파일이름_data" 폴더를 의미
        if (Directory.Exists($"{Application.dataPath}/Save"))
        {
            Debug.Log("Assets 폴더 안에 Save 폴더가 있다.");
        }
        else
        {
            Debug.Log("Assets 폴더 안에 Save 폴더가 없다.");
            Directory.CreateDirectory($"{Application.dataPath}/Save");
        }

        File.WriteAllText($"{Application.dataPath}/Save/a.txt", "Hello"); // 폴더가 없었더라도 새로 만드니까 ok.
    }

    protected override void Test5_performed(InputAction.CallbackContext context)
    {
        Debug.Log("불러오기");
        string result = File.ReadAllText("a.txt");
        Debug.Log($"{result} 읽음");

    }
#endif
}
