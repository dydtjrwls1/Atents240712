using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    // TextMeshPro => 3d Text
    // TextMeshProUGUI => Text in Canvas
    TextMeshProUGUI score;

    // 실제 점수
    int goalScore = 0;

    // 보여지는 점수
    float displayScore = 0.0f;

    // 점수가 올라가는 최소 속도 (초당 50)
    public float scoreUpMinSpeed = 50.0f;

    [Range(1.0f, 10.0f)] // 변수의 범위를 일정 범위 안으로 조정할 수 있게 해주는 attribute
    // 점수 증가 속도 변경 용
    public float scoreUpSpeedModifier = 5.0f;

    // 점수 확인 용 프로퍼티 (읽기 전용)
    public int Score
    {
        get => goalScore;
        private set // private 에서는 설정 가능
        {
            goalScore = value;

            // score.text = $"Score : {goalScore, 5}"; // 5 자리로 출력, 공백은 비워둔다.
            // score.text = $"Score : {goalScore:d5}";    // 5 자리로 출력, 공백은 0으로 채운다.
           score.text = $"{goalScore}";   // 5 자리로 출력, 공백은 0으로 채운다.
        }
    }
    
    private void Awake()
    {
        Transform child = transform.GetChild(1);

        score = child.GetComponent<TextMeshProUGUI>();

        // GetComponents<TextMeshProUGUI>(); // 불확실 하기 때문에 잘 안쓴다. 이 게임 오브젝트에 들어있는 모든 TextMeshProUGUI 찾기
        // TextMeshProUGUI[] result = GetComponentsInChildren<TextMeshProUGUI>(); // 자신과 자신의 모든 자식에 들어있는 TextMeshUGUI 찾기
    }

    private void Update()
    {
        // displayScore 가 goalScore 가 될 때 까지 계속 증가시킨다.
        if (displayScore < goalScore)
        {
            // displayScore 가 goalScore 보다 작다

            // 증가 속도 결정( goalScore 와 displayScore 의 차이가 클 수록 빠르게 증가한다, 최소치는 scoreUpMinSpeed )
            float speed = Mathf.Max((goalScore - displayScore) * scoreUpSpeedModifier, scoreUpMinSpeed);
            displayScore += Time.deltaTime * speed; // 속도에 따라 displayScore 를 증가시킨다.

            displayScore = Mathf.Min(displayScore, goalScore); // displayScore 가 goalScore 를 넘지 못하게 제한

            // UI 출력하기
            // score.text = displayScore.ToString(); // 밑과 같은 코드
            // score.text = $"{displayScore}"; 

            // score.text = $"{(int)displayScore}";  // 캐스팅으로 소수점 제거하기

            // score.text = $"{displayScore:f0}";    // 밑과 같은 코드 소수점 0째 자리까지 출력
            score.text = displayScore.ToString("f0");// 소수점 제거하기 (포맷으로 변경)

        }
    }

    public void OnInitialize()
    {
        Score = 0;
    }

    /// <summary>
    /// 점수를 증가시키는 함수
    /// </summary>
    /// <param name="point">증가시킬 양</param>
    public void AddScore(int point)
    {
        Score += point;
    }
}
