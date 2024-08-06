using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankLine : MonoBehaviour
{
    TextMeshProUGUI nametext;
    TextMeshProUGUI recordText;

    private void Awake()
    {
        Transform child = transform.GetChild(1);
        nametext = child.GetComponent<TextMeshProUGUI>();
        child = transform.GetChild(2);
        recordText = child.GetComponent<TextMeshProUGUI>();
    }

    public void SetData(string ranker, int score)
    {
        nametext.text = ranker;
        recordText.text = score.ToString("N0"); // 3자리마다 콤마 찍기
    }
}

