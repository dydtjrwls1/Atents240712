using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ImageNumber : MonoBehaviour
{
    // 숫자 스프라이트 이미지
    public Sprite[] numberImages;

    // 자리수별 이미지 컴포넌트
    Image[] digits;

    // 보여줄 숫자
    int number = -1;

    int minNum = 0;
    int maxNum = 99999;

    // 보여줄 숫자를 확인하고 설정하는 프로퍼티
    public int Number
    {
        get => number;
        set
        {
            if(number != value)
            {
                number = Mathf.Clamp(value, minNum, maxNum);

                int temp = number;
                for(int i = 0; i < digits.Length; i++)
                {
                    if(temp != 0 || i == 0) // temp 가 0 이면 보여줄 필요없지만 0이면 보여준다.
                    {
                        int digit = temp % 10;
                        digits[i].sprite = numberImages[digit];
                        digits[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        digits[i].gameObject.SetActive(false);
                    }
                    
                    temp /= 10;
                }
            }
        }
    }

    private void Awake()
    {
        digits = GetComponentsInChildren<Image>();
    }
}
