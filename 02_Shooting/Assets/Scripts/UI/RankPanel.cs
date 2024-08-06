using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RankPanel : MonoBehaviour
{
    TMP_InputField inputField;

    RankLine[] rankLines;

    // 랭킹 표시되는 사람 수
    const int MaxRankings = 5;

    /// <summary>
    /// 최고 득점자 이름과 점수 배열(정렬되어 있음)
    /// </summary>
    int[] highRecords;
    string[] rankers;

    private void Awake()
    {
        rankLines = GetComponentsInChildren<RankLine>();
        inputField = GetComponentInChildren<TMP_InputField>(true); // 비활성화 되어있는 컴포넌트를 찾으려면 파라메터를 true로 해줘야 한다.

        rankers = new string[MaxRankings];
        highRecords = new int[MaxRankings];
    }

    /// <summary>
    /// 랭킹 데이터를 초기값으로 설정하는 함수
    /// </summary>
    void SetDefaultData()
    {
        /// 1st AAA 1,000,000
        /// 2nd BBB 100,000
        /// 3rd CCC 10,000
        /// 4th DDD 1,000
        /// 5th EEE 1000

        //char defaultChar = 'A';
        //int currentScore = 1000000;

        //for(int i = 0; i < MaxRankings; i++)
        //{
        //    string name = null;

        //    for (int j = 0; j < 3; j++)
        //    {
        //        char currentChar = (char)(defaultChar + i);
        //        name += currentChar;
        //    }

        //    string score = currentScore.ToString("N0");

        //    Debug.Log($"{name} {score}");
        //    rankLines[i].SetData(name, currentScore);

        //    currentScore /= 10;
        //}

        int score = 1000000;

        for (int i = 0; i < MaxRankings; i++)
        {
            char temp = 'A';
            temp = (char)(temp + i);
            rankers[i] = $"{temp}{temp}{temp}";

            highRecords[i] = score;
            score = Mathf.RoundToInt(score * 0.1f);
        }

        RefreshRankLines();
    }

    /// <summary>
    /// 패널 갱신용 함수
    /// </summary>
    void RefreshRankLines()
    {
        for(int i = 0; i < MaxRankings; i++)
        {
            rankLines[i].SetData(rankers[i], highRecords[i]);
        }
    }

    /// <summary>
    /// 랭킹 데이터를 업데이트 하는 함수
    /// </summary>
    /// <param name="score"></param>
    void UpdateRankData(int score)
    {
        for(int i = 0; i < MaxRankings; i++)
        {
            if (highRecords[i] < score)
            {
                for(int j = MaxRankings - 1; j > i; j--) // 마지막 +1 에서부터 i 전까지 진행
                {
                    rankers[j] = rankers[j - 1];
                    highRecords[j] = highRecords[j - 1];
                }

                rankers[i] = "새 랭커";
                highRecords[i] = score;

                // inputField 의 y 값만 rankline 으로 변경하여 나타나기
                Vector3 pos = inputField.transform.position;
                pos.y = rankLines[i].transform.position.y;
                inputField.transform.position = pos;

                inputField.gameObject.SetActive(true);

                RefreshRankLines();
                break;
            }
        }
    }

#if UNITY_EDITOR
    public void Test_DefaultRankPanel()
    {
        SetDefaultData();
    }

    public void Test_UpdateRankPanel(int score)
    {
        UpdateRankData(score);
    }
#endif
}
