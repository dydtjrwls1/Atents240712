using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    // TextMeshPro => 3d Text
    // TextMeshProUGUI => Text in Canvas
    TextMeshProUGUI score;

    int goalScore = 0;

    public int Score
    {
        get => goalScore;
        set
        {
            goalScore = value;
            score.text = $"Score : {goalScore}";
        }
    }

    private void Awake()
    {
        score = GetComponent<TextMeshProUGUI>();
    }
}
