using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessManager : MonoBehaviour
{
    // vignette 정도를 조절하기 위한 커브
    public AnimationCurve curve;

    Volume postProcessVolume;

    Vignette vignette;

    private void Awake()
    {
        postProcessVolume = GetComponent<Volume>();
        postProcessVolume.profile.TryGet<Vignette>(out vignette);
    }

    private void Start()
    {
        Player player = GameManager.Instance.Player;
        player.onLifeTimeChange += OnLifeTimeChange;
    }

    // 플레이어 수명이 변경될 때마다 실행될 함수
    private void OnLifeTimeChange(float ratio)
    {
        // curve 활용
        vignette.intensity.value = curve.Evaluate(ratio);
    }
}
