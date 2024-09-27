using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LifeTime : MonoBehaviour
{
    public Gradient color;

    Slider timeSlider;
    Image fill;
    TextMeshProUGUI timeText;

    float maxLifeTime;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        timeSlider = child.GetComponent<Slider>();
        
        child = timeSlider.transform.GetChild(1);
        child = child.GetChild(0);
        fill = child.GetComponent<Image>();

        child = transform.GetChild(1);
        timeText = child.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        Player player = GameManager.Instance.Player;
        maxLifeTime = player.MaxLifeTime;
        player.onLifeTimeChange += OnLifeTimeChange;

        OnLifeTimeChange(1.0f); // 시작 시 초기화
    }

    // 플레이어 수명이 변경될 때마다 호출되는 함수
    private void OnLifeTimeChange(float ratio)
    {
        // 슬라이더값, 색, 남은시간 텍스트 변경
        timeSlider.value = ratio;
        fill.color = color.Evaluate(ratio);
        timeText.text = $"{ratio * maxLifeTime:f2}";
    }
}
