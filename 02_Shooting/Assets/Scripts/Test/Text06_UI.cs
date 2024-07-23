using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class Text06_UI : TestBase
{
    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        int score = 100;
        // 이름으로 찾기
        // GameObject obj = GameObject.Find("ScoreText"); // 문자열 비교, 이름이 중복되면 잘못 찾을 수 있음. (매우 비추천)

        // 태그로 찾기
        // 둘은 같은 함수이지만 이름만 다름
        // GameObject obj = GameObject.FindGameObjectWithTag("Tag");        // 같은 태그 중 하나만 찾기
        // GameObject[] objs = GameObject.FindGameObjectsWithTag("Tag");    // 같은 태그 모두 찾기
        // GameObject.FindWithTag; // 내부에서 FindGameObjectWithTag 를 호출하기 때문에 비추천.

        // 컴포넌트 타입으로 찾기(특정 컴포넌트로 리턴)
        //ScoreText scoreText = FindObjectOfType<ScoreText>(); // 하나만 찾기
        //ScoreText[] scoreTexts = FindObjectsByType<ScoreText>(FindObjectsInactive.Include, FindObjectsSortMode.None); // 같은 종류 모두 찾기
        //FindAnyObjectByType<ScoreText>(); // 하나만 찾기 (FindObjectOfType)보다 빠름
        //FindFirstObjectByType<ScoreText>(); // 첫 번째것 찾기(속도는 느림, 순서가 중요할 때 사용)

        ScoreText scoreText = FindAnyObjectByType<ScoreText>();
        scoreText.AddScore(score);
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        int score = 1000;
        ScoreText scoreText = FindAnyObjectByType<ScoreText>();
        scoreText.AddScore(score);
    }
}
