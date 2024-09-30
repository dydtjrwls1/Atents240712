using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCount : MonoBehaviour
{
    public float countingSpeed = 5.0f;

    float target;
    float current;

    ImageNumber imageNumber;



    private void Awake()
    {
        imageNumber = GetComponent<ImageNumber>();
    }

    private void Start()
    {
        Player player = GameManager.Instance.Player;

        player.onKillCountChange += OnKillCountChange;
    }

    private void Update()
    {
        current += Time.deltaTime * countingSpeed;
        if(current > target)
        {
            current = target;
        }
        imageNumber.Number = Mathf.FloorToInt(current);
    }

    private void OnKillCountChange(int killCount)
    {
        target = killCount;
    }

    // 숫자의 증가 속도 조절
}
