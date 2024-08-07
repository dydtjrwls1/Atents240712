using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

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

    /// <summary>
    /// null이 아니면 직전에 랭킹이 업데이트된 인덱스
    /// </summary>
    int? updatedIndex = null;

    // 세이브 파일 이름
    const string SaveFileName = "Save.json";

    private void Awake()
    {
        rankLines = GetComponentsInChildren<RankLine>();
        inputField = GetComponentInChildren<TMP_InputField>(true); // 비활성화 되어있는 컴포넌트를 찾으려면 파라메터를 true로 해줘야 한다.

        rankers = new string[MaxRankings];
        highRecords = new int[MaxRankings];

        inputField.onEndEdit.AddListener(OnNameInputEnd);           // OnEndEdit의 발동여부를 확인할 함수 등록(=onEndEdit 이벤트가 발동할 때 실행될 함수 추가)
        
    }

    private void Start()
    {
        LoadRankData();
        GameManager.Instance.Player.onDie += () => UpdateRankData(GameManager.Instance.Score); // 플레이어가 죽으면 점수 갱신
    }

    /// <summary>
    /// inputFiled 의 onEndEdit 이벤트가 발동되었을 때 실행될 함수
    /// </summary>
    /// <param name="inputText">inputFIeld 에 입력이 완료됐을 때의 입력 내용</param>
    private void OnNameInputEnd(string inputText)
    {
        inputField.gameObject.SetActive(false);
        if (updatedIndex != null)
        {
            rankers[updatedIndex.Value] = inputText;
            RefreshRankLines(updatedIndex.Value);
            SaveRankData();
            updatedIndex = null;
        }
        
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

        int score = 100000;

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
    /// 패널에서 특정 라인만 업데이트
    /// </summary>
    /// <param name="index">업데이트할 라인의 인덱스</param>
    void RefreshRankLines(int index)
    {
        rankLines[index].SetData(rankers[index], highRecords[index]);
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
                updatedIndex = i;                   // 업데이트될 인덱스
                
                // inputField 의 y 값만 rankline 으로 변경하여 나타나기
                Vector3 pos = inputField.transform.position;
                pos.y = rankLines[i].transform.position.y;
                inputField.transform.position = pos;
                inputField.text = string.Empty;
                inputField.gameObject.SetActive(true);


                RefreshRankLines();
                break;
            }
        }
    }
    void SaveRankData()
    {
        // Assets/Save 폴더에 Save.json 이라는 이름으로 저장
        SaveData data = new SaveData();
        data.rankers = rankers;
        data.highRecords = highRecords;
        string jsonText = JsonUtility.ToJson(data);

        string path = $"{Application.dataPath}/Save/";

        // 폴더가 없으면 폴더 생성
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path); 
        }

        File.WriteAllText($"{path}{SaveFileName}", jsonText);
    }

    void LoadRankData()
    {
        bool isSuccess = false;

        // Assets/Save 폴더에 Save.json 이라는 파일을 읽어온다
        string path = $"{Application.dataPath}/Save/";
        if (Directory.Exists(path))
        {
            string fullPath = $"{path}{SaveFileName}";
            if (File.Exists(fullPath))
            {
                string jsonText = File.ReadAllText(fullPath);
                SaveData data = JsonUtility.FromJson<SaveData>(jsonText);
                rankers = data.rankers;
                highRecords = data.highRecords;

                isSuccess = true;
            }
        }

        if (!isSuccess)
        {
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            SetDefaultData(); // 파일이 없다면 기본 데이터로 설정한다.
        }

        RefreshRankLines();
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

    public void Test_Save()
    {
        SaveRankData();

        //string final = null;
        //for (int i = 0; i < MaxRankings; i++)
        //{
        //    final += rankers[i] + ",";
        //    final += highRecords[i] + ",";
        //}

        //Debug.Log(final);

        //SaveData data = new SaveData();
        //data.rankers = rankers;
        //data.highRecords = highRecords;

        //string jsonText = JsonUtility.ToJson(data);
        //Debug.Log(jsonText);
    }

    public void Test_Load()
    {
        LoadRankData();

        //string jsonText = "{\"rankers\":[\"AAA\",\"BBB\",\"CCC\",\"DDD\",\"EEE\"],\"highRecords\":[1000000,100000,10000,1000,100]}";
        //SaveData data = JsonUtility.FromJson<SaveData>(jsonText);
    }
#endif
}
